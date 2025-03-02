using Application.CQRS.Image.Commands.Requests;
using Common.Exceptions;
using LibraryManagment.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private static IWebHostEnvironment _hostingEnvironment;
    private readonly ISender _sender;

    public ImageController(ISender sender, IWebHostEnvironment hostingEnvironment)
    {
        _sender = sender;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadImage formRequest)
    {
        if (formRequest.FormFile.Length == 0)
            throw new BadRequestException("Form file is null");
        try
        {
            if (!Directory.Exists(Path.Combine(_hostingEnvironment.WebRootPath, "\\Images\\")))
                Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "\\Images\\"));
            var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "\\Images\\", formRequest.FormFile.FileName);
            using (FileStream fileStream = System.IO.File.Create(fullPath))
            {
                formRequest.FormFile.CopyTo(fileStream);
                fileStream.Flush();
            }

        }
        catch (Exception ex)
        {
            throw new BadRequestException(ex.Message);
        }

        if (!System.IO.File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, "\\Images\\" + formRequest.FormFile.FileName)))
            throw new BadRequestException("File not uploaded");
        else
            Console.WriteLine("\n\n\nSalam\n\n\n");
        var request = new UploadImageRequest()
        {
            FullPath = Path.Combine(_hostingEnvironment.WebRootPath, "\\Images\\" + formRequest.FormFile.FileName)
        };
        return Ok(await _sender.Send(request));

    }


}
