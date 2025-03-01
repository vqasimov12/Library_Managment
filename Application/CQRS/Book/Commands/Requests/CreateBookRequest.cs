using Application.CQRS.Book.Commands.Responses;
using Common.GlobalResponses.Generics;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Book.Commands.Requests;
public class CreateBookRequest:IRequest<ResponseModel<CreateBookResponse>>
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int? UserId { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
    public bool ShowOnFirstScreen{ get; set; }
    public string? CoverPhoto { get; set; }
}