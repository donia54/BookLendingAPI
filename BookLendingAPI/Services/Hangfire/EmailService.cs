using BookLendingAPI.Interfaces;

namespace BookLendingAPI.Services.Hangfire
{
    public class EmailService : IEmailService
    {
        public async Task SendReminderEmail(string email, string bookTitle)
        {
            // We'll simulate the email sending here.
            await Task.Delay(500); 
            Console.WriteLine($"Email sent to {email} regarding overdue book {bookTitle}.");
        }
    }
}
