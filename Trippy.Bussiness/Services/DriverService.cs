using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Trippy.Bussiness.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repo;
        private readonly IAuditRepository _auditRepository;
        private readonly ICurrentUserService _currentUser;
        public String AuditTableName { get; set; } = "DRIVER";
        public DriverService(IDriverRepository repo, IAuditRepository auditRepository, ICurrentUserService currentUser)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
            _currentUser = currentUser;
        }
        public async Task<DriverDTO> CreateAsync(Driver driver)
        {
            driver.CompanyId = int.Parse(_currentUser.CompanyId);
            await _repo.AddAsync(driver);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<Driver>(
                tableName: AuditTableName,
                action: "create",
                recordId: driver.DriverId,
                oldEntity: null,
                newEntity: driver,
                changedBy: _currentUser.Email.ToString() // Replace with actual user info
            );
            return await ConvertDriverToDTO(driver);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var driver = await _repo.GetByIdAsync(id);
            if (driver == null) return false;
            _repo.Delete(driver);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<Driver>(
                tableName: AuditTableName,
                action: "Delete",
                recordId: driver.DriverId,
                oldEntity: driver,
                newEntity: driver,
                changedBy: _currentUser.Email.ToString() // Replace with actual user info
            );
            return true;
        }

        public  async Task<List<DriverDTO>> GetAllAsync()
        {


           // List<DriverDTO> driverdtos = new List<DriverDTO>();

            var drivers =  await _repo.GetQuerableDriverList();
            
            //foreach (var driver in drivers)
            //{
            //    DriverDTO driverDTO = await ConvertDriverToDTO(driver);
            //    driverdtos.Add(driverDTO);


            //}

            return await drivers.ToListAsync ();
        }

        //private static DriverDTO ConvertDriverToDTO(Driver driver)
        //{
        //    DriverDTO driverDTO = new DriverDTO();
        //    driverDTO.DriverId = driver.DriverId;
        //    driverDTO.DriverName = driver.DriverName;
        //    driverDTO.Nationality = driver.Nationality;
        //    driverDTO.License = driver.License;
        //    driverDTO.ImageSrc = driver.ImageSrc;
        //    driverDTO.IsActive = driver.IsActive;
        //    driverDTO.IsRented = driver.IsRented;
        //    driverDTO.DOB = driver.DOB;
        //    driverDTO.DOBString = driver.DOB.HasValue ? driver.DOB.Value.ToString("dd MMMM yyyy hh:mm tt") : "";

            

        //    return driverDTO;
        //}

        private  async  Task<DriverDTO> ConvertDriverToDTO(Driver driver)
        {
            DriverDTO driverDTO = new DriverDTO();
            driverDTO.DriverId = driver.DriverId;
            driverDTO.DriverName = driver.DriverName;
            driverDTO.Nationality = driver.Nationality;
            driverDTO.NationalId = driver.NationalId;
            driverDTO.License = driver.License;
            driverDTO.ProfileImagePath = driver.ProfileImagePath;
            driverDTO.IsActive = driver.IsActive;
            driverDTO.IsRented = driver.IsRented;
            driverDTO.DOB = driver.DOB;
            driverDTO.ContactNumber = driver.ContactNumber;
            driverDTO.CompanyId = driver.CompanyId;
            // driverDTO.AuditLog = await _auditRepository.GetAuditLogsForEntityAsync("Driver", driver.DriverId);


            return driverDTO;
        }

        public async Task<DriverDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if(q == null) return null;
            var dirverdto = await ConvertDriverToDTO(q);
            //dirverdto.AuditLog = await _auditRepository.GetAuditLogsForEntityAsync("Drivers", dirverdto.DriverId);

            return dirverdto;
        }

        public async Task<bool> UpdateAsync(Driver driver)
        {
            var oldentity = await _repo.GetByIdAsync(driver.DriverId);
            _repo.Detach(oldentity);
            _repo.Update(driver);
            driver.CompanyId = int.Parse(_currentUser.CompanyId);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<Driver>(
               tableName: AuditTableName,
               action: "update",
               recordId: driver.DriverId,
               oldEntity: oldentity,
               newEntity: driver,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;
        }

        public async Task<CustomApiResponse> UpdateProfilePicAsync(int Driverid, string profileImagePath)
        {
            var user = await _repo.GetByIdAsync(Driverid);
            if (user == null)
                return new CustomApiResponse { IsSucess = false, Error = "User not found", StatusCode = 404 };

            user.ProfileImagePath = profileImagePath;
             _repo.Update(user);
            await _repo.SaveChangesAsync();

            return new CustomApiResponse { IsSucess = true, Value = profileImagePath, StatusCode = 200 };
        }
    }
}
