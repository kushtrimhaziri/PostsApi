using FluentValidation;

namespace PostsApi.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty();
        
        RuleFor(v => v.Title)
            .MaximumLength(1000)
            .NotEmpty();
    }
}

