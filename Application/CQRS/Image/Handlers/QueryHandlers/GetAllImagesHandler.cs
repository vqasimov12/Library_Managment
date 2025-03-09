using Application.CQRS.Image.Queries.Requests;
using Application.CQRS.Image.Queries.Responses;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Image.Handlers.QueryHandlers;

public sealed class GetAllImagesHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllImagesRequest, ResponseModelPagination<GetImagesResponse>>
{
    public async Task<ResponseModelPagination<GetImagesResponse>> Handle(GetAllImagesRequest request, CancellationToken cancellationToken)
    {
        var images = await unitOfWork.ImageRepository.GetAllImagesAsync();

        if (!images.Any())
            return new ResponseModelPagination<GetImagesResponse>
            {
                Data =
                {
                    Data=[],
                    TotalCount = 0
                },
                Errors = [],
                IsSuccess = true
            };

        var totalCount = images.Count();
        images = images.Skip((request.Page - 1) * request.Limit).Take(request.Limit);

        var list = images.Select(image => new GetImagesResponse
        {
            ImagePath = image.ImagePath,
            ImageId = image.PhotoId
        }).ToList();

        return new ResponseModelPagination<GetImagesResponse>
        {
            Data = new Pagination<GetImagesResponse>
            {
                Data = list,
                TotalCount = totalCount
            },
            Errors = [],
            IsSuccess = true
        };
    }
}
