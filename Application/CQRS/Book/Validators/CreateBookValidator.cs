using Application.CQRS.Book.Commands.Requests;
using FluentValidation;

namespace Application.CQRS.Book.Validators;

public class CreateBookValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(25);
        RuleFor(x => x.Author).NotEmpty().MinimumLength(2).MaximumLength(25);
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Language).NotEmpty().IsInEnum();
        RuleFor(x => x.ShowOnFirstScreen).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(255);
    }

}
