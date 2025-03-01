using Application.CQRS.Book.Commands.Requests;
using Application.CQRS.Book.Commands.Responses;
using AutoMapper;
using Common.GlobalResponses.Generics;
using FluentValidation;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Book.Handlers.CommandHandlers;

public class CreateBookHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateBookRequest> validator) : IRequestHandler<CreateBookRequest, ResponseModel<CreateBookResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateBookRequest> _validator = validator;

    public async Task<ResponseModel<CreateBookResponse>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var mappedRequest = _mapper.Map<Domain.Entities.Book>(request);
        mappedRequest.CreatedDate = DateTime.Now;
        mappedRequest.CreatedBy = 1;
        Console.WriteLine(1);
        await _unitOfWork.BookRepository.AddBookAsync(mappedRequest);
        Console.WriteLine(2);
        await _unitOfWork.SaveChangesAsync();
        Console.WriteLine(3);

        return new ResponseModel<CreateBookResponse>
        {
            Data = _mapper.Map<CreateBookResponse>(mappedRequest),
            Errors = [],
            IsSuccess = true
        };
    }
}