using BookLendingAPI.Dtos;
using BookLendingAPI.Middlewares;

namespace BookLendingAPI.Interfaces
{
    public interface IBorrowingService
    {
        Task<ResponseResult<List<BorrowingDto>>> GetUserBorrowingsAsync(string userId);
        Task<ResponseResult<BorrowingDto>> BorrowBookAsync(BorrowingCreateDto dto);
        Task<ResponseResult> ReturnBookAsync(int borrowingId);


    }
}
