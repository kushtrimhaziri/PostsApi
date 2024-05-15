using FluentValidation;

namespace PostsApi.Application.Admin.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty();
        
        RuleFor(v => v.Password)
            .NotEmpty();
    }
}

