using FluentValidation;

namespace PostsApi.Application.Posts.Commands.UpdatePost;


public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(v => v.Content)
            .MaximumLength(1000)
            .NotEmpty();
    }
}
