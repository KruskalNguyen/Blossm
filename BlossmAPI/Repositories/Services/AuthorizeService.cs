using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using System.Linq;

namespace BlossmAPI.Repositories.Services
{
    public class AuthorizeService:IAuthorizeServices
    {
        private readonly BlossmContext _context;

        public AuthorizeService(BlossmContext context)
        {
            _context = context;
        }

        public async Task<List<Access>> GetAllPrivileges()
        {
            try
            {
                var accesses = await _context.Accesses.ToListAsync();
                return accesses;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Access>> GetPrivilegesFromUser(string id)
        {
            try
            {
                var accesses = _context.Accesses.Include(a => a.IdUsers)
                    .Where(a => a.IdUsers.Any(u => u.Id.Equals(id))).ToList();

                foreach (var a in accesses)
                {
                    a.IdUsers.Clear();
                }

                return accesses;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Access>> GetNonPrivilegesFromUser(string id)
        {
            try
            {
                var allList = await GetAllPrivileges();
                var userList = await GetPrivilegesFromUser(id);

                var result = allList.Except(userList).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> GrantPrivilegesToUser(AccessUserView accessUser)
        {
            try
            {
                var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id.Equals(accessUser.Id_User));

                if (user == null) return false;

                var listAccess = await _context.Accesses.Where(a => accessUser.Id_Access.Contains(a.Id)).ToListAsync();
                user.IdAccesses = listAccess;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            
        }

        public async Task<bool> RemoveAllPrivilegesOfUser(string id_user)
        {
            try
            {
                var user = await _context.AspNetUsers.Include(u => u.IdAccesses).FirstOrDefaultAsync(u => u.Id.Equals(id_user));

                if (user == null) return false;

                user.IdAccesses.Clear();

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<bool> RemovePrivilegesOfUser(AccessUserView accessUser)
        {
            try
            {
                var user = await _context.AspNetUsers.Include(u => u.IdAccesses).FirstOrDefaultAsync(u => u.Id.Equals(accessUser.Id_User));

                if (user == null) return false;

                var itemsToRemove = user.IdAccesses
                    .Where(item => accessUser.Id_Access.Contains(item.Id))
                    .ToList();

                foreach (var itemToRemove in itemsToRemove)
                {
                    user.IdAccesses.Remove(itemToRemove);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<Access>> GetPrivilegesFromRole(string id)
        {
            try
            {
                var accesses = _context.Accesses.Include(a => a.IdRoles)
                    .Where(a => a.IdRoles.Any(u => u.Id.Equals(id))).ToList();

                foreach (var a in accesses)
                {
                    a.IdRoles.Clear();
                }

                return accesses;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Access>> GetNonPrivilegesFromRole(string id)
        {
            try
            {
                var allList = await GetAllPrivileges();
                var roleList = await GetPrivilegesFromRole(id);

                var result = allList.Except(roleList).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> CreateRole(RoleView mvRole)
        {
            try
            {
                AspNetRole role = new AspNetRole();
                role.Id = mvRole.Id;
                role.Name = mvRole.Name;
                role.NormalizedName = mvRole.NormalizedName;
                role.ConcurrencyStamp = mvRole.ConcurrencyStamp;

                await _context.AspNetRoles.AddAsync(role);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> GrantPrivilegesToRole(AccessRoleView accessRole)
        {
            try
            {
                var role = await _context.AspNetRoles.FirstOrDefaultAsync(u => u.Id.Equals(accessRole.Id_Role));

                if (role == null) return false;

                var listAccess = await _context.Accesses.Where(a => accessRole.Id_Access.Contains(a.Id)).ToListAsync();
                role.IdAccesses = listAccess;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> RomoveRole(string id_role)
        {
            try
            {
                var role = await _context.AspNetRoles.Include(u => u.IdAccesses).FirstOrDefaultAsync(u => u.Id == id_role);

                if (role == null) return false;

                role.IdAccesses.Clear();

                _context.AspNetRoles.Remove(role);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemovePrivilegesOfRole(AccessRoleView accessRole)
        {
            try
            {
                var role = await _context.AspNetRoles.Include(u => u.IdAccesses).FirstOrDefaultAsync(u => u.Id.Equals(accessRole.Id_Role));

                if (role == null) return false;

                var itemsToRemove = role.IdAccesses
                    .Where(item => accessRole.Id_Access.Contains(item.Id))
                    .ToList();

                foreach (var itemToRemove in itemsToRemove)
                {
                    role.IdAccesses.Remove(itemToRemove);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> GrantRoleToUser(UserRoleView userRole)
        {
            try
            {
                var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id.Equals(userRole.Id_User));
                List<List<Access>> listAccess = new List<List<Access>>(); 

                if (user == null) return false;

                foreach (var item in userRole.Id_Role)
                {
                    var access = await _context.AspNetRoles.Include(r => r.IdAccesses).FirstOrDefaultAsync(r => r.Id.Equals(item));
                    listAccess.Add(access.IdAccesses.ToList());
                    user.Roles.Add(access);
                }

                await _context.SaveChangesAsync();

                List<Access> combinedList = listAccess.SelectMany(list => list).Distinct().ToList();


                foreach (var item in combinedList)
                {
                    user.IdAccesses.Add(item);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

       
    }
}
