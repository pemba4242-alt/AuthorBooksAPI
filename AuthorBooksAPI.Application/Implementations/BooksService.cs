using AuthorBooksAPI.Application.DTO;
using AuthorBooksAPI.Application.Interfaces;
using AuthorBooksAPI.Domain.Entities;
using AuthorBooksAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorBooksAPI.Application.Implementations
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsRepository _authorsRepository;

        public BooksService(IBooksRepository booksRepository, IAuthorsRepository authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
           var books = await _booksRepository.GetAllAsync();
           return books.Select(x => new BookDto
           {
               BookId = x.BookId,
               Title = x.Title,
               BookNumber = x.BookNumber,
               PublishedYear = x.PublishedYear,
               CreatedDate = x.CreatedDate
           }).ToList();

        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _booksRepository.GetByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            return new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                BookNumber = book.BookNumber,
                PublishedYear = book.PublishedYear,
                CreatedDate = book.CreatedDate
            };
        }

        public async Task<bool> CreateAsync(BookRequestDto bookRequestDto)
        {
            var authors = await ResolveAuthorsAsync(bookRequestDto.AuthorIds);
            var book = new Book { 
                Title = bookRequestDto.Title,
                BookNumber = bookRequestDto.BookNumber,
                PublishedYear = bookRequestDto.PublishedYear,
                CreatedDate = DateTime.Now,
                Authors = authors
            };

            return await _booksRepository.CreateAsync(book);
        }

        private async Task<List<Author>> ResolveAuthorsAsync(List<int> ids)
        {
            var distinct = ids.Distinct().ToList();
            var found = await _authorsRepository.GetByIdsAsync(distinct);   // soft-deleted authors are not found
            var missing = distinct.Except(found.Select(a => a.AuthorId)).ToList();
            if (missing.Count > 0)
                throw new Exception($"Author id(s) not found: {string.Join(", ", missing)}.");
            return found;
        }
    }
}
