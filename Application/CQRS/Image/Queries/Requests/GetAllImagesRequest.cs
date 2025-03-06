using Application.CQRS.Image.Queries.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Image.Queries.Requests;

public record  GetAllImagesRequest : IRequest<ResponseModelPagination<GetImagesResponse>>
{
    public int Page { get; set; }
    public int Limit { get; set; }
}
