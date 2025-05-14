using BookLendingAPI.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace BookLendingAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        // This method handles the response based on the status code returned.
        // It checks the status code and returns the appropriate HTTP response.

        public string CurrentUserId => HttpContext.Items["UserId"] as string ?? string.Empty;

        protected ActionResult MapResponseToActionResult<T>(ResponseResult<T> response)
        {
            try
            {
                switch (response.StatusCode)
                {
                    case StatusCodes.Status200OK:
                        return Ok(response.Data);

                    case StatusCodes.Status201Created:
                        return Created(string.Empty, response.Data);

                    case StatusCodes.Status204NoContent:
                        return NoContent();

                    case StatusCodes.Status400BadRequest:
                        return BadRequest(response.ErrorMessage);

                    case StatusCodes.Status401Unauthorized:
                        return Unauthorized(response.ErrorMessage);

                    case StatusCodes.Status403Forbidden:
                        return StatusCode(StatusCodes.Status403Forbidden, response.ErrorMessage);

                    case StatusCodes.Status404NotFound:
                        return NotFound(response.ErrorMessage);

                    case StatusCodes.Status405MethodNotAllowed:
                        return StatusCode(StatusCodes.Status405MethodNotAllowed, response.ErrorMessage);

                    case StatusCodes.Status409Conflict:
                        return Conflict(response.ErrorMessage);

                    case StatusCodes.Status422UnprocessableEntity:
                        return UnprocessableEntity(response.ErrorMessage);

                    case StatusCodes.Status500InternalServerError:
                        return StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage);

                    case StatusCodes.Status502BadGateway:
                        return StatusCode(StatusCodes.Status502BadGateway, response.ErrorMessage);

                    case StatusCodes.Status503ServiceUnavailable:
                        return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);

                    case StatusCodes.Status504GatewayTimeout:
                        return StatusCode(StatusCodes.Status504GatewayTimeout, response.ErrorMessage);

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError,
                                           $"Unexpected error: {response.ErrorMessage ?? "An unexpected error occurred."}");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request");
            }
        }

        protected ActionResult MapResponseToActionResult(ResponseResult response)
        {
            try
            {
                switch (response.StatusCode)
                {
                    case StatusCodes.Status200OK:
                        return Ok();

                    case StatusCodes.Status201Created:
                        return Created();

                    case StatusCodes.Status204NoContent:
                        return NoContent();

                    case StatusCodes.Status400BadRequest:
                        return BadRequest(response.ErrorMessage);

                    case StatusCodes.Status401Unauthorized:
                        return Unauthorized(response.ErrorMessage);

                    case StatusCodes.Status403Forbidden:
                        return Forbid();

                    case StatusCodes.Status404NotFound:
                        return NotFound(response.ErrorMessage);

                    case StatusCodes.Status405MethodNotAllowed:
                        return StatusCode(StatusCodes.Status405MethodNotAllowed, response.ErrorMessage);

                    case StatusCodes.Status409Conflict:
                        return Conflict(response.ErrorMessage);

                    case StatusCodes.Status422UnprocessableEntity:
                        return UnprocessableEntity(response.ErrorMessage);

                    case StatusCodes.Status500InternalServerError:
                        return StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage);

                    case StatusCodes.Status502BadGateway:
                        return StatusCode(StatusCodes.Status502BadGateway, response.ErrorMessage);

                    case StatusCodes.Status503ServiceUnavailable:
                        return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);

                    case StatusCodes.Status504GatewayTimeout:
                        return StatusCode(StatusCodes.Status504GatewayTimeout, response.ErrorMessage);

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError,
                                           $"Unexpected error: {response.ErrorMessage ?? "An unexpected error occurred."}");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request");
            }
        }
    }
}
