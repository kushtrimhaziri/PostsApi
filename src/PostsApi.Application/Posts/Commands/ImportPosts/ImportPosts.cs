using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Posts.Commands.ImportPosts;

[Authorize(Roles = Roles.Administrator)]
public record ImportPostsCommand : IRequest<int>
{
    public IFormFile? File { get; set; }
}


public class ImportPostsCommandHandler : IRequestHandler<ImportPostsCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Post> _postRepository;
    private readonly IBackgroundJobProcessor _backgroundJobProcessor;
    private readonly IPostService _postService;
    
    public ImportPostsCommandHandler(IUnitOfWork unitOfWork, IBackgroundJobProcessor backgroundJobProcessor, IPostService postService)
    {
        _unitOfWork = unitOfWork;
        _backgroundJobProcessor = backgroundJobProcessor;
        _postService = postService;
        _postRepository = _unitOfWork.GetRepository<Post>();
    }

    public async Task<int> Handle(ImportPostsCommand request, CancellationToken cancellationToken)
    {
        using (var memoryStream = new MemoryStream())
        {
            await request.File?.CopyToAsync(memoryStream, cancellationToken)!;
            var fileContent = memoryStream.ToArray();

            // Enqueue the Hangfire job with file content
            _backgroundJobProcessor.Enqueue( () => _postService.ImportPosts(fileContent));

        }

        return 0;
    }
}
