using Application.CQRS.Book.Queries.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Book.Queries.Requests;

public class GetAllBooksRequest:IRequest<ResponseModelPagination<GetAllBooksResponse>>
{
    public int Limit { get; set; }
    public int Page { get; set; }
}
