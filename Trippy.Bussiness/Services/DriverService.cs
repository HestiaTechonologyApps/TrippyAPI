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
        public DriverService(IDriverRepository repo)
        {
            _repo = repo;
        }
        public async Task<DriverDTO> CreateAsync(Driver driver)
        {
            await _repo.AddAsync(driver);
            await _repo.SaveChangesAsync();
            return ConvertDriverToDTO(driver);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var driver = await _repo.GetByIdAsync(id);
            if (driver == null) return false;
            _repo.Delete(driver);
            await _repo.SaveChangesAsync();
            return true;
        }

        public  async Task<List<DriverDTO>> GetAllAsync()
        {


            List<DriverDTO> driverdtos = new List<DriverDTO>();

            var drivers =  await _repo.GetAllAsync();
            
            foreach (var driver in drivers)
            {
                DriverDTO driverDTO = ConvertDriverToDTO(driver);
                driverdtos.Add(driverDTO);


            }

            return driverdtos;
        }

        private static DriverDTO ConvertDriverToDTO(Driver driver)
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
            return driverDTO;
        }

        public async Task<DriverDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            return q == null ? null : ConvertDriverToDTO(q);
        }

        public async Task<bool> UpdateAsync(Driver coupon)
        {
            _repo.Update(coupon);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
