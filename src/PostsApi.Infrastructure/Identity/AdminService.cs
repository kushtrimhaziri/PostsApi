using Microsoft.AspNetCore.Identity;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Domain;

namespace PostsApi.Infrastructure.Identity;

 public class AdminService : IAdminService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;


        public AdminService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddRole(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            var userCreated = await _userManager.CreateAsync(user, password);
            return userCreated;
        }

        public async Task<IdentityResult> DeleteUser(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityUser> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> FindUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public IQueryable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<IList<string>> GetAllRolesOfUser(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }


        public IQueryable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users;
        }

        public async Task<bool> IsInRoleAsync(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveAllRolesOfUser(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var l = await _userManager.RemoveFromRolesAsync(user, roles);

            return l;
        }

        public async Task<IdentityResult> UpdateUser(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        async Task<bool> IAdminService.IsInRoleAsync(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
    }
