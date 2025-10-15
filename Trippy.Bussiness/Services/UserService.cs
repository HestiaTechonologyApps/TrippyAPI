using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;


namespace Trippy.Bussiness.Services
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IAuditRepository _auditRepository;

        public UserService(IUserRepository repo, IAuditRepository auditRepository   )
        {
            _repo = repo;
            _auditRepository = auditRepository;
        }
        public async Task<UserDTO> CreateAsync(User user)
        {
            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<User>(
               tableName: "Users",
               action: "create",
               recordId: user.UserId,
               oldEntity: null,
               newEntity: user,
               changedBy: "System" // Replace with actual user info
           );
            return await ConvertUserToDTO(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;
            _repo.Delete(user);
            await _auditRepository.LogAuditAsync<User>(
               tableName: "Users",
               action: "Delete",
               recordId: user.UserId,
               oldEntity: user,
               newEntity: user,
               changedBy: "System" // Replace with actual user info
           );
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {


            List<UserDTO> userdtos = new List<UserDTO>();

            var users = await _repo.GetAllAsync();

            foreach (var user in users)
            {
                UserDTO userDTO = await ConvertUserToDTO(user);
                userdtos.Add(userDTO);


            }

            return userdtos;
        }

        private async Task <UserDTO> ConvertUserToDTO(User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserId = user.UserId;
            userDTO.UserName = user.UserName;
            userDTO.UserEmail = user.UserEmail;
            userDTO.Address = user.Address;
            userDTO.PhoneNumber = user.PhoneNumber;
            userDTO.Islocked = user.Islocked;
            userDTO.CreateAt = user.CreateAt;
            userDTO.Lastlogin = user.Lastlogin;
            userDTO.LastloginString = user.Lastlogin.HasValue ? user.Lastlogin.Value.ToString("dd MMMM yyyy hh:mm tt") : "";
            userDTO.CreateAtSyring = user.CreateAt.ToString("dd MMMM yyyy hh:mm tt");


            userDTO.IsActive = user.IsActive;
            userDTO.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync("User", user.UserId);
            return userDTO;
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var userdto = await  ConvertUserToDTO(q);
            userdto.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync("Users", userdto.UserId);
            return userdto;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var oldentity = await _repo.GetByIdAsync(user.UserId);
            _repo.Detach(oldentity);
            _repo.Update(user);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<User>(
               tableName: "Users",
               action: "update",
               recordId: user.UserId,
               oldEntity: oldentity,
               newEntity: user,
               changedBy: "System" // Replace with actual user info
           );
            return true;
        }
    }
}
