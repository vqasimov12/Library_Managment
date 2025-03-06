using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Image.Commands.Requests;

public record DeleteImageRequest : IRequest<ResponseModel<string>>
{
    public string ImageId { get; set; }
}
