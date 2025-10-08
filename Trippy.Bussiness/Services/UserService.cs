using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRIPPY.DOMAIN.DTO;
using Trippy.Domain.Entities;
using TRIPPY.DOMAIN.Interfaces.IRepositories;
using TRIPPY.DOMAIN.Entities;
using Trippy.Domain.DTO;

namespace TRIPPY.BUSSINESS.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<UserDTO> CreateAsync(User user)
        {
            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();
            return ConvertUserToDTO(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;
            _repo.Delete(user);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {


            List<UserDTO> userdtos = new List<UserDTO>();

            var users = await _repo.GetAllAsync();

            foreach (var user in users)
            {
                UserDTO userDTO = ConvertUserToDTO(user);
                userdtos.Add(userDTO);


            }

            return userdtos;
        }

        private UserDTO ConvertUserToDTO(User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserId = user.UserId;
            userDTO.UserName = user.UserName;
            userDTO.UserEmail = user.UserEmail;
            userDTO.Address = user.Address;
            userDTO.PhoneNumber = user.PhoneNumber;
            userDTO.PasswordHash = user.PasswordHash;
            userDTO.Islocked = user.Islocked;
            userDTO.CreateAt = user.CreateAt;
            userDTO.Lastlogin = user.Lastlogin;
            userDTO.IsActive = user.IsActive;
            return userDTO;
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            return q == null ? null : ConvertUserToDTO(q);
        }


       
    }
}
