using MediatR;
using PostsApi.Application.Admin.Commands.AssignUserRole;
using PostsApi.Application.Admin.Commands.CreateUser;
using PostsApi.Application.Admin.Commands.DeleteAllRolesOfUser;
using PostsApi.Application.Admin.Commands.DeleteUser;
using PostsApi.Application.Admin.Commands.UpdateUser;
using PostsApi.Application.Admin.Queries;
using PostsApi.Application.Admin.Queries.GetRolesOfUser;
using PostsApi.Application.Admin.Queries.GetRolesWithPagination;
using PostsApi.Application.Admin.Queries.GetUserByEmail;
using PostsApi.Application.Admin.Queries.GetUserById;
using PostsApi.Application.Admin.Queries.GetUsersWithPagination;
using PostsApi.Application.Common.Models;
using PostsApiW.Infrastructure;

namespace PostsApiW.Endpoints;

public class Admin : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetUserByEmail, "/GetByEmail")
            .MapGet(GetUserById, "/GetById")
            .MapGet(GetAllRolesOfUser, "/RolesOfUser")
            .MapGet(GetUsersWithPagination, "/Users")
            .MapGet(GetRolesWithPagination, "/Roles")
            .MapPost(CreateUser, "/User")
            .MapPost(AssignUserRole, "/AssignUserRole")
            .MapPut(UpdateUser, "/User/{id}")
            .MapDelete(DeleteUser, "/User/{id}")
            .MapDelete(DeleteAllRolesOfUser, "/AllRoles/{id}");
    }

    public Task<UserDto> GetUserByEmail(ISender sender, string email)
    {
        return sender.Send(new GetUserByEmailQuery(email));
    }

    public Task<UserDto> GetUserById(ISender sender, string id)
    {
        return sender.Send(new GetUserByIdQuery(id));
    }

    public Task<IList<string>> GetAllRolesOfUser(ISender sender, string userId)
    {
        return sender.Send(new GetRolesOfUserQuery(userId));
    }

    public Task<PaginatedList<UserDto>> GetUsersWithPagination(ISender sender,
        [AsParameters] GetUsersWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<PaginatedList<RoleDto>> GetRolesWithPagination(ISender sender,
        [AsParameters] GetRolesWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateUser(ISender sender, CreateUserCommand command)
    {
        return sender.Send(command);
    }


    public Task<int> AssignUserRole(ISender sender, AssignUserRoleCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateUser(ISender sender, string id, UpdateUserCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteUser(ISender sender, string id)
    {
        await sender.Send(new DeleteUserCommand(id));
        return Results.NoContent();
    }
    
    public async Task<IResult> DeleteAllRolesOfUser(ISender sender, string id)
    {
        await sender.Send(new DeleteAllRolesOfUserCommand(id));
        return Results.NoContent();
    }
}
