using System.ComponentModel.DataAnnotations;

namespace BookLendingAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public required string Title { get; set; }

        [Required, MaxLength(100)]
        public string? Author { get; set; }

        [MaxLength(20)]
        public string? ISBN { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
