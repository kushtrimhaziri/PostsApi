using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Posts.Commands.UpdatePost;

[Authorize(Roles = Roles.Administrator)]
public record UpdatePostCommand : IRequest
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }

}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Post> _postRepository;

    public UpdatePostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _postRepository = _unitOfWork.GetRepository<Post>();
    }

    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _postRepository.GetById(request.Id);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Content = request.Content;
        
        _unitOfWork.Save();
    }
}
