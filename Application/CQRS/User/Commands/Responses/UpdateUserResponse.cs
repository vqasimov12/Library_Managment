using Domain.Enums;

namespace Application.CQRS.User.Commands.Responses;

public class UpdateUserResponse
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Fathername { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string MobilePhone { get; set; }
    public string CardNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public DateTime DateOfDismissal { get; set; }
    public string Note { get; set; }
    public int CreatedBy { get; set; }
    public Gender Gender { get; set; }
    public UserType UserType { get; set; }
}
