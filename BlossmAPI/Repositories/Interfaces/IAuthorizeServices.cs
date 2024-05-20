using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IAuthorizeServices
    {
        Task<bool> GrantPrivilegesToUser(AccessUserView accessUser);
        Task<bool> RemoveAllPrivilegesOfUser(string id_user);
        Task<bool> RemovePrivilegesOfUser(AccessUserView accessUser);
        Task<bool> CreateRole(RoleView mvRole);
        Task<bool> GrantRoleToUser(UserRoleView userRole);
        Task<bool> GrantPrivilegesToRole(AccessRoleView accessRole);
        Task<bool> RomoveRole(string id_role);
        Task<bool> RemovePrivilegesOfRole(AccessRoleView accessRole);
        Task<List<Access>> GetAllPrivileges();
        Task<List<Access>> GetPrivilegesFromUser(string id);
        Task<List<Access>> GetNonPrivilegesFromUser(string id);
        Task<List<Access>> GetPrivilegesFromRole(string id);
        Task<List<Access>> GetNonPrivilegesFromRole(string id);
    }
}
