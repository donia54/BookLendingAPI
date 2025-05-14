using System.ComponentModel.DataAnnotations;

namespace BookLendingAPI.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class BookCreateDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string? Author { get; set; }

        [MaxLength(20)]
        public string? ISBN { get; set; }
    }

    public class BookUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string? Author { get; set; }

        [MaxLength(20)]
        public string? ISBN { get; set; }

        public bool IsAvailable { get; set; } = true;
    }


}
