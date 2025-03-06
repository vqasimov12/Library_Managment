using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Image.Queries.Requests;

public record GetImageRequest:IRequest<ResponseModel<string>>
{
    public string ImageId { get; set; }
}
