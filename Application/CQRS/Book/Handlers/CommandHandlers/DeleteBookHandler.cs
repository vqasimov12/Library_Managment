using Application.CQRS.Book.Commands.Requests;
using Application.CQRS.Book.Commands.Responses;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Book.Handlers.CommandHandlers;

public class DeleteBookHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookRequest, ResponseModel<DeleteBookResponse>>
{
    public async Task<ResponseModel<DeleteBookResponse>> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.BookRepository.GetBookByIdAsync(request.Id);
        if (book is null)
            throw new BadRequestException("Book not found with provided id");

        await unitOfWork.BookRepository.DeleteBookAsync(book.Id,1);
        await unitOfWork.SaveChangesAsync();

        return new ResponseModel<DeleteBookResponse>
        {
            Data = new DeleteBookResponse
            {
                Message = "Book deleted successfully"
            },
            Errors = [],
            IsSuccess = true
        };
    }
}
