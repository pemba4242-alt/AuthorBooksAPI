using AuthorBooksAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorBooksAPI.Infrastructure.Interfaces
{
    public interface IAuthorsRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<List<Author>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
