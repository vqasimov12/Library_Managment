using Application.CQRS.Image.Commands.Requests;
using Application.CQRS.Image.Queries.Requests;
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
        if (formRequest.FormFile == null || formRequest.FormFile.Length == 0)
            throw new BadRequestException("Form file is null or empty");

        try
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(formRequest.FormFile.FileName)}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await formRequest.FormFile.CopyToAsync(stream);

            var request = new UploadImageRequest()
            {
                ImagePath = filePath,
            };

            return Ok(await _sender.Send(request));
        }
        catch (Exception ex)
        {
            throw new BadRequestException(ex.Message);
        }
    }


    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var request = new GetImageRequest()
        {
            ImageId = id
        };
        return Ok(await _sender.Send(request));
    }

    [HttpGet]
    public async Task<IActionResult> GetImages([FromQuery] GetAllImagesRequest request) => Ok(await _sender.Send(request));

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] DeleteImageRequest request) => Ok(await _sender.Send(request));

    [HttpGet("download")]
    public async Task<FileStreamResult> Download([FromQuery] DownloadImageRequest request) => await _sender.Send(request);

}
