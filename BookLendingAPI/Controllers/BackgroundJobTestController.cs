using BookLendingAPI.Services.Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLendingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundJobTestController : ControllerBase
    {
        private readonly BackgroundJobs _backgroundJobs;

        public BackgroundJobTestController(BackgroundJobs backgroundJobs)
        {
            _backgroundJobs = backgroundJobs;
        }

       
        [HttpPost("testDelayedBooksJob")]
        public async Task<IActionResult> TriggerDelayedBooksJob()
        {
         
            await _backgroundJobs.CheckDelayedBooksJob();

            return Ok("Job triggered manually");
        }
    }
}
