namespace BookLendingAPI.Models
{
    public partial class Borrowing
    {
        public int Id { get; set; }
        public  string UserId { get; set; } = null!;
        public int BookId { get; set; }

        public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; } = false;

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Book Book { get; set; } = null!;
    }

}
