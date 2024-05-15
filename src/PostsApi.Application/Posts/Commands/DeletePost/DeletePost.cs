using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Posts.Commands.DeletePost;

[Authorize(Roles = Roles.Administrator)]
public record DeletePostCommand(int Id) : IRequest;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Post> _postRepository;

    public DeletePostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _postRepository = _unitOfWork.GetRepository<Post>();
    }

    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        // var entity = await _context.TodoItems
        //     .FindAsync(new object[] { request.Id }, cancellationToken);

        var entity = await _postRepository.GetById(request.Id);
        
        Guard.Against.NotFound(request.Id, entity);

        _postRepository.Delete(entity);

        _unitOfWork.Save();
    }
}
