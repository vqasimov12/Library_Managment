using Application.CQRS.User.Queries.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.User.Queries.Requests;

public class GetUserByIdRequest:IRequest<ResponseModel<GetUserByIdResponse>>
{
    public int Id { get; set; }
}
