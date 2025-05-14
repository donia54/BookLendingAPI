using BookLendingAPI.Data;
using BookLendingAPI.Interfaces;
using BookLendingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookLendingAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Borrowings).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Borrowings)
                                       .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByConditionAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _context.Books.FirstOrDefaultAsync(predicate);
        }


        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Borrowing>> GetDelayedBooksAsync()
        {
            var today = DateTime.UtcNow;


            var delayedBooks = await _context.Borrowings
                .Include(b => b.Book)
               .Where(b => b.DueDate < today && !b.Returned)
                .ToListAsync();

            return delayedBooks;
        }


    }
}
