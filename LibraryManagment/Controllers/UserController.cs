using Application.CQRS.User.Commands.Requests;
using Application.CQRS.User.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery]GetAllUsersRequest request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var request = new GetUserByIdRequest { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        => Ok(await _sender.Send(request));

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var request = new DeleteUserRequest { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        => Ok(await _sender.Send(request));

}
