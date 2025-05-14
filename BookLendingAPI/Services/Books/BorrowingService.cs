using AutoMapper;
using BookLendingAPI.Dtos;
using BookLendingAPI.Interfaces;
using BookLendingAPI.Middlewares;
using BookLendingAPI.Models;
using BookLendingAPI.Repositories;

namespace BookLendingAPI.Services.Books
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BorrowingService(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IMapper mapper, IUserService userService)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _userService = userService;
        }


        public async Task<ResponseResult<BorrowingDto>> BorrowBookAsync(BorrowingCreateDto dto)
        {
            var userDto = await _userService.IsUserExistAsync(dto.UserId);
            if (userDto == null)
            {
                return ResponseResult<BorrowingDto>.NotFound("User not found.");
            }

            var validationResult = await _ValidateBorrowingRequest(dto);
            if (validationResult != null)
                return validationResult;

            var borrowing = await _CreateBorrowingRecord(dto);
            await _UpdateBookAvailability(borrowing.BookId, false);

            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
            borrowingDto.User = userDto;
            return ResponseResult<BorrowingDto>.Created(borrowingDto);
        }




        public async Task<ResponseResult> ReturnBookAsync(int borrowingId)
        {
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(borrowingId);
            if (borrowing == null)
                return ResponseResult.NotFound("Borrowing record not found.");

            if (borrowing.Returned)
                return ResponseResult.Error("Book already returned.", StatusCodes.Status400BadRequest);

            borrowing.Returned = true;
            await _UpdateBookAvailability(borrowing.BookId, true);

            await _borrowingRepository.SaveChangesAsync();
            return ResponseResult.Success();
        }



        public async Task<ResponseResult<List<BorrowingDto>>> GetUserBorrowingsAsync(string userId)
        {
            try
            {
                var userDto = await _userService.IsUserExistAsync(userId);
                if (userDto == null)
                {
                    return ResponseResult<List<BorrowingDto>>.NotFound("User not found.");
                }
                var borrowings = await _borrowingRepository.GetByConditionAsync(b => b.UserId == userId && !b.Returned);

                if (borrowings == null || borrowings.Count == 0)
                {
                    return ResponseResult<List<BorrowingDto>>.NotFound("No borrowings found for this user.");
                }


                var borrowingDtos = new List<BorrowingDto>();
                foreach (var borrowing in borrowings)
                {
                    var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
                    var user = await _userService.GetUserByIdAsync(borrowing.UserId);  
                    borrowingDto.User = _mapper.Map<UserDto>(user);  
                    borrowingDtos.Add(borrowingDto);
                }

                return ResponseResult<List<BorrowingDto>>.Success(borrowingDtos);
            }
            catch (System.Exception ex)
            {
                return ResponseResult<List<BorrowingDto>>.FromException(ex);
            }
        }

        private async Task<ResponseResult<BorrowingDto>>_ValidateBorrowingRequest(BorrowingCreateDto dto)
        {
          
            var activeBorrowing = await _borrowingRepository.GetActiveBorrowingByUserAsync(dto.UserId);
            if (activeBorrowing != null)
                return ResponseResult<BorrowingDto>.Error("User already has a borrowed book.", StatusCodes.Status400BadRequest);

           
            var book = await _bookRepository.GetByIdAsync(dto.BookId);
            if (book == null)
                return ResponseResult<BorrowingDto>.NotFound("Book not found.");

            if (!book.IsAvailable)
                return ResponseResult<BorrowingDto>.Error("Book is currently unavailable.", StatusCodes.Status400BadRequest);

            return null;
        }



        private async Task<Borrowing>_CreateBorrowingRecord(BorrowingCreateDto dto)
        {
            var borrowing = new Borrowing
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
                BorrowedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                Returned = false
            };

            await _borrowingRepository.AddAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            return borrowing;
        }

        private async Task _UpdateBookAvailability(int bookId, bool isAvailable)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                book.IsAvailable = isAvailable;
                await _bookRepository.SaveChangesAsync();
            }
        }
    }
}
