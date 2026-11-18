using AuthorBooksAPI.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorBooksAPI.Application.Interfaces
{
    public interface IAuthorsService
    {
        Task<List<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
    }
}
