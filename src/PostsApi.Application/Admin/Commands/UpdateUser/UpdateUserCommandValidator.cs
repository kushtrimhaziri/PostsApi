using FluentValidation;

namespace PostsApi.Application.Admin.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty();
    }
}
