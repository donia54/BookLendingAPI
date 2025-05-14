using BookLendingAPI.Dtos;
using BookLendingAPI.Interfaces;
using BookLendingAPI.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLendingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ResponseResult<List<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetAllAsync();
            return MapResponseToActionResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseResult<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _bookService.GetByIdAsync(id);
            return MapResponseToActionResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseResult<BookDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] BookCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseResult.Error("Invalid data", StatusCodes.Status400BadRequest));

            var result = await _bookService.CreateAsync(dto);
            return MapResponseToActionResult(result);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseResult<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] BookUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseResult.Error("Invalid data", StatusCodes.Status400BadRequest));

            var result = await _bookService.UpdateAsync(dto);
            return MapResponseToActionResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);
            return MapResponseToActionResult(result);
        }

    }
}
