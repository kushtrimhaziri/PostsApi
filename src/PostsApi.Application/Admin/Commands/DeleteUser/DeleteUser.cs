using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;

namespace PostsApi.Application.Admin.Commands.DeleteUser;

[Authorize(Roles = Roles.Administrator)]
public record DeleteUserCommand(string Id) : IRequest<bool>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IAdminService _adminService;

    public DeleteUserCommandHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _adminService.FindUserById(request.Id);

        return user != null ? (await _adminService.DeleteUser(user)).Succeeded : false;
    }
}
