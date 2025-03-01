using Application.CQRS.Book.Commands.Requests;
using Application.CQRS.Book.Queries.Requests;
using Application.CQRS.User.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery]GetAllBooksRequest request)=>Ok(await _sender.Send(request));

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
        => Ok(await _sender.Send(request));

    [HttpPut]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
        => Ok(await _sender.Send(request));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var request = new GetBookByIdRequest { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook([FromQuery] DeleteBookRequest request)=> Ok(await _sender.Send(request));
}