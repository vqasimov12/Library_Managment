using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Application.CQRS.Image.Queries.Requests;

public class DownloadImageRequest:IRequest<FileStreamResult>
{
    public string ImageId { get; set; }
}
