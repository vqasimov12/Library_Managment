using Application.CQRS.User.Queries.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.User.Queries.Requests;

public sealed class GetAllUsersRequest:IRequest<ResponseModelPagination<GetAllUsersResponse>>
{
    public int Limit { get; set; } = 10;
    public int Page { get; set; } = 1;
}
