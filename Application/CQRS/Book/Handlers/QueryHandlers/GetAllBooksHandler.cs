using Application.CQRS.Book.Queries.Requests;
using Application.CQRS.Book.Queries.Responses;
using Application.CQRS.User.Queries.Responses;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;
using System.Runtime.CompilerServices;

namespace Application.CQRS.Book.Handlers.QueryHandlers;

public class GetAllBooksHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllBooksRequest, ResponseModelPagination<GetAllBooksResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModelPagination<GetAllBooksResponse>> Handle(GetAllBooksRequest request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.BookRepository.GetAll();
        if (!books.Any())
            return new ResponseModelPagination<GetAllBooksResponse>()
            {
                Data = null,
                Errors = [],
                IsSuccess = true
            };
        var totalCount = books.Count();
        books = books.Skip((request.Page - 1) * request.Limit).Take(request.Limit);
        var mappedBooks = new List<GetAllBooksResponse>();
        foreach (var book in books)
            mappedBooks.Add(_mapper.Map<GetAllBooksResponse>(book));

        return new ResponseModelPagination<GetAllBooksResponse>
        {
            Data = new Pagination<GetAllBooksResponse>
            {
                Data = mappedBooks,
                TotalCount = totalCount
            },
            Errors = [],
            IsSuccess = true
        };
    }
}