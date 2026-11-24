using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthorBooksAPI.Application.DTO
{
    public class BookRequestDto
    {
        [Required]
        [Length(4, 200)]
        public string Title { get; set; }

        [Required]
        [Length(4, 20)]
        public string BookNumber { get; set; }

        [Required]
        [Range(1900, 2026, ErrorMessage = "Published year must be between 1900 and 2026.")]
        public int PublishedYear { get; set; }

        public List<int> AuthorIds { get; set; }
    }
}
