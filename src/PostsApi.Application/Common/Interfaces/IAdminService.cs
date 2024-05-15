using Microsoft.AspNetCore.Identity;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Common.Interfaces;

public interface IAdminService
{
    Task<IdentityUser> FindByEmail(string email);
    IQueryable<IdentityUser> GetAllUsers();
    IQueryable<IdentityRole> GetAllRoles();
    Task<IdentityResult> CreateUser(User user, string password);
    Task<User> FindUserById(string id);
    Task<IdentityResult> UpdateUser(User user);
    Task<bool> IsInRoleAsync(User user, string role);
    Task<IdentityResult> AddRole(User user, string role);
    Task<IdentityResult> DeleteUser(User user);
    Task<IdentityResult> RemoveAllRolesOfUser(User user);
    Task<IList<string>> GetAllRolesOfUser(User user);
}
