using Application.CQRS.User.Commands.Requests;
using FluentValidation;

namespace Application.CQRS.User.Validators;

public class UpdateUserValidator:AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(z => z.Name).NotEmpty().MaximumLength(25).MinimumLength(3)
            .Matches("^[A-Za-z]+(?:\\s[A-Za-z]+)*\\s*$");

        RuleFor(z => z.Fathername).NotEmpty().MaximumLength(25).MinimumLength(3)
            .Matches("^[A-Za-z]+(?:\\s[A-Za-z]+)*\\s*$");

        RuleFor(z => z.Username).NotEmpty().MaximumLength(25).MinimumLength(3)
            .Matches("[A-Za-z][A-Za-z0-9_]");

        RuleFor(z => z.Email).NotEmpty().EmailAddress();

        RuleFor(z => z.Address).NotEmpty().MaximumLength(255).MinimumLength(3);

        RuleFor(z => z.MobilePhone).NotEmpty().MaximumLength(20).Matches(@"^\+994(5[015]|7[07])\d{7}$");

        RuleFor(z => z.CardNumber).Length(16).Matches(@"^\d{16}$");

        RuleFor(z => z.BirthDay).NotEmpty().LessThan(DateTime.Now);

        RuleFor(z => z.DateOfEmployment).NotEmpty().LessThan(DateTime.Now);

        RuleFor(z => z.DateOfDismissal).LessThan(DateTime.Now);

        RuleFor(z => z.Note).MaximumLength(255);

        RuleFor(z => z.CreatedBy).NotEmpty().GreaterThan(0);

        RuleFor(z => z.Gender).IsInEnum();

        RuleFor(z => z.UserType).IsInEnum();

        RuleFor(z => z).Must(z => z.DateOfDismissal > z.DateOfEmployment);
        RuleFor(z => z).Must(z => z.DateOfEmployment > z.BirthDay);

        RuleFor(z => z).Must(z => z.DateOfDismissal > z.BirthDay);

    }
}
