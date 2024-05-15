using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;

namespace PostsApi.Application.Admin.Commands.DeleteAllRolesOfUser;

[Authorize(Roles = Roles.Administrator)]
public record DeleteAllRolesOfUserCommand(string Id) : IRequest<bool>;

public class DeleteAllRolesOfUserCommandHandler : IRequestHandler<DeleteAllRolesOfUserCommand, bool>
{
    private readonly IAdminService _adminService;

    public DeleteAllRolesOfUserCommandHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<bool> Handle(DeleteAllRolesOfUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _adminService.FindUserById(request.Id);

        return user != null ? (await _adminService.RemoveAllRolesOfUser(user)).Succeeded : false;
    }
}
