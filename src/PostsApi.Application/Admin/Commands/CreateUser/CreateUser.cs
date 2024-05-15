using MediatR;
using Microsoft.AspNetCore.Identity;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Application.Common.Security;
using PostsApi.Core.Constants;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Admin.Commands.CreateUser;

[Authorize(Roles = Roles.Administrator)]
public record CreateUserCommand : IRequest<int>
{
    public string? Email { get; init; }
    public string? Password { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IAdminService _adminService;

    public CreateUserCommandHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User { Email = request.Email, UserName = request.Email };

        var created = await _adminService.CreateUser(entity, request.Password);

        return created.GetHashCode();
    }
}
