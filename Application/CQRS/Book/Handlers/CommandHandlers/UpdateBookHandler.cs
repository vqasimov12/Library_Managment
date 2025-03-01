
using Application.CQRS.Book.Commands.Requests;
using Application.CQRS.Book.Commands.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using FluentValidation;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Book.Handlers.CommandHandlers;

public class UpdateBookHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateBookRequest> validator) : IRequestHandler<UpdateBookRequest, ResponseModel<UpdateBookResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateBookRequest> _validator = validator;

    public async Task<ResponseModel<UpdateBookResponse>> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.BookRepository.GetBookByIdAsync(request.Id);
        if (book is null)
            throw new BadRequestException("Book not found with provided id");

        book = _mapper.Map<Domain.Entities.Book>(request);
        book.UpdatedBy = 1;
        book.UpdatedDate = DateTime.Now;

        await _unitOfWork.SaveChangesAsync();

        return new ResponseModel<UpdateBookResponse>
        {
            Data = _mapper.Map<UpdateBookResponse>(book),
            Errors = [],
            IsSuccess = true
        };


    }
}
