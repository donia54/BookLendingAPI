using BookLendingAPI.Dtos;
using BookLendingAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLendingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : BaseController
    {

        private readonly IBorrowingService _borrowingService;

        public BorrowingController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }


        [HttpPost]
        public async Task<ActionResult> BorrowBook([FromBody] BorrowingCreateDto dto)
        {
            var response = await _borrowingService.BorrowBookAsync(dto);
            return MapResponseToActionResult(response);
        }

        [HttpPut("retur-book{borrowingId}")]
        public async Task<ActionResult> ReturnBook(int borrowingId)
        {
            var response = await _borrowingService.ReturnBookAsync(borrowingId);
            return MapResponseToActionResult(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetUserBorrowings(string userId)
        {
            var response = await _borrowingService.GetUserBorrowingsAsync(userId);

            return MapResponseToActionResult(response);
        }
    }
}
