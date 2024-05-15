using MediatR;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Application.Common.Security;
using PostsApi.Core.Constants;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Posts.Commands.CreatePost;

[Authorize(Roles = Roles.Administrator)]
public record CreatePostCommand : IRequest<int>
{
    public string? Title { get; init; }
    public string? Content { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Post> _postRepository;
    private readonly ISlugService _slugService;
    
    public CreatePostCommandHandler(IUnitOfWork unitOfWork,ISlugService slugService)
    {
        _unitOfWork = unitOfWork;
        _postRepository = _unitOfWork.GetRepository<Post>();
        _slugService = slugService;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = new Post
        {
            Title = request.Title,
            Content = request.Content,
            FriendlyUrl = await _slugService.GenerateSlug(request.Title)
        };

        await _postRepository.Add(entity);
        _unitOfWork.Save();

        return entity.Id;
    }
}
