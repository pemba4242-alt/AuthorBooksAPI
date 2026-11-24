using AuthorBooksAPI.Domain.Entities;
using AuthorBooksAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorBooksAPI.Infrastructure.Implementations
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly AuthorBooksAPIDbContext _authorBooksAPIDbContext;

        public AuthorsRepository(AuthorBooksAPIDbContext authorBooksAPIDbContext)
        {
            _authorBooksAPIDbContext = authorBooksAPIDbContext;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorBooksAPIDbContext.Authors.Where(x => x.DeletedDate == null).Include(x => x.Books).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _authorBooksAPIDbContext.Authors.Where(x => x.DeletedDate == null && x.AuthorId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Author>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var list = ids.ToList();
            return await _authorBooksAPIDbContext.Authors.Where(a => list.Contains(a.AuthorId)).ToListAsync();
        }
    }
}
