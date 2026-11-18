using AuthorBooksAPI.Application.DTO;
using AuthorBooksAPI.Application.Interfaces;
using AuthorBooksAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorBooksAPI.Application.Implementations
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsService(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var authors = await _authorsRepository.GetAllAsync();
            return authors.Select(x => new AuthorDto
            {
                AuthorId = x.AuthorId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address
            }).ToList();

        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await _authorsRepository.GetByIdAsync(id);
            if (author == null)
            {
                return null;
            }
            return new AuthorDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Address = author.Address
            };
        }
    }
}
