
using Application.CQRS.Book.Queries.Requests;
using Application.CQRS.Book.Queries.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Book.Handlers.QueryHandlers;

public class GetBookByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetBookByIdRequest, ResponseModel<GetBookByIdResponse>>
{
    public async Task<ResponseModel<GetBookByIdResponse>> Handle(GetBookByIdRequest request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.BookRepository.GetBookByIdAsync(request.Id);
        if (book == null)
            throw new BadRequestException("Book with provided id can not found");

        return new ResponseModel<GetBookByIdResponse>
        {
            Data = mapper.Map<GetBookByIdResponse>(book),
            Errors = [],
            IsSuccess = true
        };
    }
}
