namespace BookLendingAPI.Dtos
{
    public class BorrowingCreateDto
    {
        public int BookId { get; set; }
        public string UserId { get; set; } = null!;
    }

    public class BorrowingDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public UserDto User { get; set; } = null!;
        public DateTime BorrowedAt { get; set; }
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }
    }


}
