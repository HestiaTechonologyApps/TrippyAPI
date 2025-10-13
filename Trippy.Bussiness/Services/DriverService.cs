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

namespace Trippy.Bussiness.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repo;
        private readonly IAuditRepository _auditRepository;

        public DriverService(IDriverRepository repo, IAuditRepository auditRepository)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
        }
        public async Task<DriverDTO> CreateAsync(Driver driver)
        {
            await _repo.AddAsync(driver);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<Driver>(
                tableName: "Drivers",
                action: "create",
                recordId: driver.DriverId,
                oldEntity: null,
                newEntity: driver,
                changedBy: "System" // Replace with actual user info
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
                tableName: "Drivers",
                action: "Delete",
                recordId: driver.DriverId,
                oldEntity: null,
                newEntity: driver,
                changedBy: "System" // Replace with actual user info
            );
            return true;
        }

        public  async Task<List<DriverDTO>> GetAllAsync()
        {


            List<DriverDTO> driverdtos = new List<DriverDTO>();

            var drivers =  await _repo.GetAllAsync();
            
            foreach (var driver in drivers)
            {
                DriverDTO driverDTO = await ConvertDriverToDTO(driver);
                driverdtos.Add(driverDTO);


            }

            return driverdtos;
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
            driverDTO.License = driver.License;
            driverDTO.ImageSrc = driver.ImageSrc;
            driverDTO.IsActive = driver.IsActive;
            driverDTO.IsRented = driver.IsRented;
            driverDTO.DOB = driver.DOB;
            driverDTO.DOBString = driver.DOB.HasValue ? driver.DOB.Value.ToString("dd MMMM yyyy hh:mm tt") : "";

            driverDTO.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync("Driver", driver.DriverId);


            return driverDTO;
        }

        public async Task<DriverDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if(q == null) return null;
            var dirverdto = await ConvertDriverToDTO(q);
            dirverdto.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync("Driver", dirverdto.DriverId);

            return dirverdto;
        }

        public async Task<bool> UpdateAsync(Driver coupon)
        {
            _repo.Update(coupon);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
