using BookLendingAPI.Dtos;
using BookLendingAPI.Middlewares;
using BookLendingAPI.Models;

namespace BookLendingAPI.Interfaces
{
    public interface IBookService
    {
        Task<ResponseResult<IEnumerable<BookDto>>> GetAllAsync();
        Task<ResponseResult<BookDto>> GetByIdAsync(int id);
        Task<ResponseResult<BookDto>> CreateAsync(BookCreateDto dto);
        Task<ResponseResult<BookDto>> UpdateAsync(BookUpdateDto dto);
        Task<ResponseResult> DeleteAsync(int id);

        Task<IEnumerable<BorrowingDto>> GetDelayedBooksAsync();
    }
}
