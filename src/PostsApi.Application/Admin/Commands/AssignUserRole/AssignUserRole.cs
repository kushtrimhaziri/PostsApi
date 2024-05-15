using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;

namespace PostsApi.Application.Admin.Commands.AssignUserRole;

[Authorize(Roles = Roles.Administrator)]
public class AssignUserRoleCommand : IRequest<int>
{
    public string? UserId { get; init; }
    public string? Role { get; set; }
}

public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, int>
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public AssignUserRoleCommandHandler(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }

    public async Task<int> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _adminService.FindUserById(request.UserId);
        if (user != null)
        {
            var created = await _adminService.AddRole(user, request.Role);
            return created.GetHashCode();
        }

        return 0;
    }
}
