using AuthorBooksAPI.Application.DTO;
using AuthorBooksAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorBooksAPI.Application
{
    public static class Mappings
    {
        public static BookDto ToDto(this Book b)
        {
            return new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                BookNumber = b.BookNumber,
                PublishedYear = b.PublishedYear
            };
        }

        public static AuthorDto ToDto(this Author a)
        {
            return new AuthorDto
            {
                AuthorId = a.AuthorId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Address = a.Address,
                Books = a.Books.Select(x => x.ToDto()).ToList()
            };
        }
    }
}