namespace BookLendingAPI.Dtos
{
    public class AuthDTO
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime ExpiresOn { get; set; }

    }
}
