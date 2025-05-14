using BookLendingAPI.Models;
using System.Linq.Expressions;

namespace BookLendingAPI.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);

        Task<IEnumerable<Borrowing>> GetDelayedBooksAsync();
        Task<Book?> GetByConditionAsync(Expression<Func<Book, bool>> predicate);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task SaveChangesAsync();

    

    }
}