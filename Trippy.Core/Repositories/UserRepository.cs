using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<UserListDTO> QueryableUserList()
        {
            return from usr in _context.Users.AsNoTracking()
                   join cmp in _context.Companies.AsNoTracking()
                   on usr.CompanyId equals cmp.CompanyId
                   select new UserListDTO
                   {
                       UserId = usr.UserId,
                       UserName = usr.UserName,
                       UserEmail = usr.UserEmail,
                       PhoneNumber = usr.PhoneNumber,
                       CompanyName = cmp.ComapanyName,
                       IsActive = usr.IsActive
                       
                       
                   };
        }

        //public async Task<List<UserListDTO>> GetUserListAsync()
        //{
        //    IQueryable<UserListDTO> q = QueryableUserList();
        //    return await q.ToListAsync();
        //}



        public async Task<UserDTO> GetUserDetailsAsync(int userId)
        {
            var q = (from user in _context.Users
                     join comp in _context.Companies
                         on user.CompanyId equals comp.CompanyId
                     where user.UserId == userId
                     select new UserDTO
                     {
                         UserId = user.UserId,
                         UserName = user.UserName,
                         UserEmail = user.UserEmail,
                         PhoneNumber = user.PhoneNumber,
                         Address = user.Address,
                         IsActive = user.IsActive,
                         Islocked = user.Islocked,
                         Lastlogin = user.Lastlogin,
                         CreateAt = user.CreateAt,
                         CompanyId = user.CompanyId,
                         CompanyName = comp.ComapanyName,
                         

                        
                     }).FirstAsync();

            return await q;
        }

        public async Task<List<UserListDTO>> GetUsersAsync()
        {
            IQueryable<UserListDTO> q = QueryableUserList();

            return await q.ToListAsync();
        }

        public async Task AddLoginLogAsync(UserLoginLog log)
        {
            await _context.UserLoginLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }


        public async Task<List<UserLoginLogDTO>> GetUserLogsAsync(int userId)
        {
            return await _context.UserLoginLogs
       .Where(x => x.UserId == userId)
       .OrderByDescending(x => x.ActionTime)
       .Select(x => new UserLoginLogDTO
       {
           UserLoginLogId = x.UserLoginLogId,
           UserId = x.UserId,
           ActionType = x.ActionType,
           ActionTimeString = x.ActionTime.ToString("dd MMM yyyy HH:mm:ss")
       })
       .ToListAsync();
        }
    }
}
