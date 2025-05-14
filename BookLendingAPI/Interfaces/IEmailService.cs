namespace BookLendingAPI.Interfaces
{
    public interface IEmailService
    {
        Task SendReminderEmail(string email, string bookTitle);
    }
}
