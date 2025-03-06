using Common.GlobalResponses.Generics;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Image.Commands.Requests;

public class UploadImageRequest : IRequest<ResponseModel<string>>
{
    public string ImagePath { get; set; }
}
