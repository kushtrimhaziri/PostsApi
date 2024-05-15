using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Application.Common.Mappings;
using PostsApi.Application.Common.Models;

namespace PostsApi.Application.Admin.Queries.GetUsersWithPagination;

public record GetUsersWithPaginationQuery : IRequest<PaginatedList<UserDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class
    GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery,
    PaginatedList<UserDto>>
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public GetUsersWithPaginationQueryHandler(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserDto>> Handle(GetUsersWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _adminService.GetAllUsers()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
