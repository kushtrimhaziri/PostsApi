using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Application.Common.Mappings;
using PostsApi.Application.Common.Models;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Posts.Queries.GetPostsWithPagination;

public record GetPostsWithPaginationQuery : IRequest<PaginatedList<PostDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class
    GetPostsWithPaginationQueryHandler : IRequestHandler<GetPostsWithPaginationQuery,
    PaginatedList<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Post> _postRepository;

    private readonly IMapper _mapper;

    public GetPostsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _postRepository = _unitOfWork.GetRepository<Post>();
        _mapper = mapper;
    }

    public async Task<PaginatedList<PostDto>> Handle(GetPostsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _postRepository.GetAllAsQueryAble()
            .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
