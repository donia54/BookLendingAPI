using BookLendingAPI.Interfaces;

namespace BookLendingAPI.Services.Hangfire
{
    public class BackgroundJobs
    {
        private readonly IReminderService _reminderService;

        public BackgroundJobs(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        public async Task CheckDelayedBooksJob()
        {
            await _reminderService.CheckDelayedBooks();
        }
    }

}
