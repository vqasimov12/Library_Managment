using Application.CQRS.Book.Commands.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Book.Commands.Requests;

public class DeleteBookRequest:IRequest<ResponseModel<DeleteBookResponse>>
{
    public int Id { get; set; }
}
