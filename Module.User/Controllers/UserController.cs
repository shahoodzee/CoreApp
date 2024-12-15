using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.User.Core.Commands;
using Module.User.Core.Queries;

namespace Module.User.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
internal class UserController : ControllerBase
{
    private readonly IMediator mediator;

    public UserController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [AllowAnonymous]
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        try
        {
            command.origin = HttpContext.Request.Headers["Origin"];
            var response = await mediator.Send(command);
            if (response.Data)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(LoginUserCommand command)
    {
        try
        {
            var response = await mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(long userId)
    {
        try
        {
            var response = await mediator.Send(new GetUserByIdQuery() { userId = userId });
            if (response.Data != null)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPatch("UpdateUser")]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        try
        {
            var mxuiClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "mxui");
            if (mxuiClaim != null)
            {
                command.iModifiedBy = long.Parse(mxuiClaim.Value);
            }
            var response = await mediator.Send(command);
            if (response.Data)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("ActivateUser")]
    public async Task<IActionResult> ActivateUser(ActivateUserCommand command)
    {
        try
        {
            var response = await mediator.Send(command);
            if (response.Data)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetAllCustomers")]
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            var response = await mediator.Send(new GetAllCustomersQuery());
            if (response.Data != null)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetAllExpertSupport")]
    public async Task<IActionResult> GetAllExpertSupport()
    {
        try
        {
            var response = await mediator.Send(new GetAllExpertSupportQuery());
            if (response.Data != null)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetAllOnlineExpertSupport")]
    public async Task<IActionResult> GetAllOnlineExpertSupport()
    {
        try
        {
            var response = await mediator.Send(new GetAllOnlineExpertSupportQuery());
            if (response.Data != null)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [AllowAnonymous]
    [HttpPost("ForgetPassword")]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordCommand command)
    {
        try
        {
            command.origin = HttpContext.Request.Headers["Origin"];
            var response = await mediator.Send(command);
            if (response.Data)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [AllowAnonymous]
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        try
        {
            var response = await mediator.Send(command);
            if (response.Data)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [AllowAnonymous]
    [HttpPost("LogoutUser")]
    public async Task<IActionResult> LogoutUser(LogoutUserCommand command)
    {
        try
        {
            var response = await mediator.Send(command);
            if (response.Data != false)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}
