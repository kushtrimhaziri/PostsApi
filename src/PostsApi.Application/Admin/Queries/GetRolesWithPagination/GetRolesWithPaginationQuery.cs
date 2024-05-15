using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Application.Common.Mappings;
using PostsApi.Application.Common.Models;

namespace PostsApi.Application.Admin.Queries.GetRolesWithPagination;

public record GetRolesWithPaginationQuery : IRequest<PaginatedList<RoleDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class
    GetRolesWithPaginationQueryHandler : IRequestHandler<GetRolesWithPaginationQuery,
    PaginatedList<RoleDto>>
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public GetRolesWithPaginationQueryHandler(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoleDto>> Handle(GetRolesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _adminService.GetAllRoles()
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
