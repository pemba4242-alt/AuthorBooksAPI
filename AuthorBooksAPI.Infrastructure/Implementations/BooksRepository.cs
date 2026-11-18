using AuthorBooksAPI.Domain.Entities;
using AuthorBooksAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthorBooksAPI.Infrastructure.Implementations
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AuthorBooksAPIDbContext _authorBooksAPIDbContext;

        public BooksRepository(AuthorBooksAPIDbContext authorBooksAPIDbContext)
        {
            _authorBooksAPIDbContext = authorBooksAPIDbContext;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _authorBooksAPIDbContext.Books.Where(x => x.DeletedDate == null).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _authorBooksAPIDbContext.Books.Where(x => x.DeletedDate == null && x.BookId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(Book book)
        {
            _authorBooksAPIDbContext.Books.Add(book);
            var result = await _authorBooksAPIDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
