using AutoMapper;
using BookLendingAPI.Dtos;
using BookLendingAPI.Interfaces;
using BookLendingAPI.Middlewares;
using BookLendingAPI.Models;

namespace BookLendingAPI.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }



        public async Task<ResponseResult<IEnumerable<BookDto>>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<BookDto>>(books);
            return ResponseResult<IEnumerable<BookDto>>.Success(result);
        }

        public async Task<ResponseResult<BookDto>> GetByIdAsync(int id) 
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return ResponseResult<BookDto>.Error("Book not found", StatusCodes.Status404NotFound);

            return ResponseResult<BookDto>.Success(_mapper.Map<BookDto>(book));
        }


        public async Task<ResponseResult<BookDto>> CreateAsync(BookCreateDto dto)
        {

            if(await _IsISBNExist(dto.ISBN))
                return ResponseResult<BookDto>.Error("A book with the same ISBN already exists.", StatusCodes.Status400BadRequest);

            var book = _mapper.Map<Book>(dto);
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return ResponseResult<BookDto>.Success(_mapper.Map<BookDto>(book));
        }


        public async Task<ResponseResult<BookDto>> UpdateAsync(BookUpdateDto dto)
        {
            var existing = await _bookRepository.GetByIdAsync(dto.Id);
            if (existing == null)
                return ResponseResult<BookDto>.Error("Book not found", StatusCodes.Status404NotFound);


            if (await _IsISBNExist(dto.ISBN))
                return ResponseResult<BookDto>.Error("A book with the same ISBN already exists.", StatusCodes.Status400BadRequest);


            _mapper.Map(dto, existing);
            await _bookRepository.UpdateAsync(existing);
            await _bookRepository.SaveChangesAsync();

            return ResponseResult<BookDto>.Success(_mapper.Map<BookDto>(existing));
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return ResponseResult.Error("Book not found", StatusCodes.Status404NotFound);

            await _bookRepository.DeleteAsync(book);
            await _bookRepository.SaveChangesAsync();

            return ResponseResult.Success();
        }

        public async Task<IEnumerable<BorrowingDto>> GetDelayedBooksAsync()
        {
            var delayedBooks = await _bookRepository.GetDelayedBooksAsync();

            var delayedBooksDto = _mapper.Map<IEnumerable<BorrowingDto>>(delayedBooks);

            return delayedBooksDto;
        }

        private async Task<bool> _IsISBNExist(string? isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;

            var existingBook = await _bookRepository.GetByConditionAsync(b => b.ISBN == isbn);
            return existingBook != null;
        }

    }
}
