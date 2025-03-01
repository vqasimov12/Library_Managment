using Application.CQRS.Book.Queries.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Book.Queries.Requests;

public class GetBookByIdRequest:IRequest<ResponseModel<GetBookByIdResponse>>
{
    public int Id { get; set; }
}
