using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Admin.Commands.UpdateUser;

[Authorize(Roles = Roles.Administrator)]

public class UpdateUserCommand : IRequest<bool>
{
    public string Id { get; init; }
    public string Email { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,bool>
{
    private readonly IAdminService _adminService;


    public UpdateUserCommandHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User { Email = request.Email, UserName = request.Email };

        var existinguser = await _adminService.FindUserById(request.Id);

        if (existinguser != null)
        {
            existinguser.Email = request.Email;
            existinguser.UserName = request.Email;
            var updated = await _adminService.UpdateUser(existinguser);

            return updated.Succeeded;
        }

        return false;
    }
}
