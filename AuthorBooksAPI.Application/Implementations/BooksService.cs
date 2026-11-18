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

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
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
            var book = new Book { 
                Title = bookRequestDto.Title,
                BookNumber = bookRequestDto.BookNumber,
                PublishedYear = bookRequestDto.PublishedYear,
                CreatedDate = DateTime.Now
            };

            return await _booksRepository.CreateAsync(book);
        }
    }
}
