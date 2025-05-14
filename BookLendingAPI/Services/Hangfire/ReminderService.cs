using BookLendingAPI.Interfaces;

namespace BookLendingAPI.Services.Hangfire
{
    public class ReminderService : IReminderService
    {
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;
        private readonly ILogger<ReminderService> _logger;

        public ReminderService(IBookService bookService, IEmailService emailService, ILogger<ReminderService> logger)
        {
            _bookService = bookService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task CheckDelayedBooks()
        {

            var delayedBooks = await _bookService.GetDelayedBooksAsync();

            foreach (var borrowing in delayedBooks)
            {

                var userEmail = borrowing.User.Email;

                // Simulate sending an email reminder to the user
                await _emailService.SendReminderEmail(userEmail, borrowing.BookTitle);

                _logger.LogInformation($"Reminder sent to user {userEmail} regarding overdue book '{borrowing.BookTitle}'.");

            }
        }
    }
}
