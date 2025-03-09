using Application.CQRS.Image.Queries.Requests;
using Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Repository.Common;

namespace Application.CQRS.Image.Handlers.QueryHandlers;

public class DownloadImageHandler(IUnitOfWork unitOfWork, FileExtensionContentTypeProvider _contentTypeProvider) : IRequestHandler<DownloadImageRequest, FileStreamResult>
{
    public async Task<FileStreamResult> Handle(DownloadImageRequest request, CancellationToken cancellationToken)
    {
        var image = await unitOfWork.ImageRepository.GetImageByIdAsync(request.ImageId);
        if (image == null)
            throw new BadRequestException("Image not found with provided id");

        var fileBytes = await File.ReadAllBytesAsync(image.ImagePath, cancellationToken);
        var memoryStream = new MemoryStream(fileBytes);

        return new FileStreamResult(memoryStream, GetMimeType(image.ImagePath))
        {
            FileDownloadName = Path.GetFileName(image.ImagePath) 
        };
    }

    private string GetMimeType(string filename)
    {
        if (!_contentTypeProvider.TryGetContentType(filename, out var mimeType))
            throw new BadRequestException($"Unsupported file type, file: {filename}");
        return mimeType;
    }
}
