using Application.CQRS.User.Commands.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.User.Commands.Requests;

public class DeleteUserRequest:IRequest<ResponseModel<DeleteUserResponse>>
{
    public int Id { get; set; }
}
