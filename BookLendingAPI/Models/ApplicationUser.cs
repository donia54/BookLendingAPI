using Microsoft.AspNetCore.Identity;

namespace BookLendingAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }

}