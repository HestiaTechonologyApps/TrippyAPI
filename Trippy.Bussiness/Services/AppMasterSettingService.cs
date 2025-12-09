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
    public class AppMasterSettingService : IAppMasterSettingService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppMasterSettingRepository _repo;
        private readonly IAuditRepository _auditRepo;
        public String AuditTableName { get; set; } = "APPMSTERSETTING";
        public AppMasterSettingService(ICurrentUserService currentUserService, IAppMasterSettingRepository repo, IAuditRepository auditRepository)
        {
            _currentUserService = currentUserService;
            _repo = repo;
            _auditRepo = auditRepository;
        }
        public async Task<AppMasterSettingDTO> CreateAsync(AppMasterSetting req)
        {
            await _repo.AddAsync(req);
            await _repo.SaveChangesAsync();

            return new AppMasterSettingDTO
            {
                AppMasterSettingId = req.AppMasterSettingId,
                CurrentCompanyId = req.CurrentCompanyId,
                IntCurrentFinancialYear = req.IntCurrentFinancialYear,
                IsActive = req.IsActive,
                Staff_To_User_Rate_Per_Second = req.Staff_To_User_Rate_Per_Second,
                one_paisa_to_coin_rate = req.one_paisa_to_coin_rate
            };
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<List<AppMasterSettingDTO>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();

            return data.Select(x => new AppMasterSettingDTO
            {
                AppMasterSettingId = x.AppMasterSettingId,
                CurrentCompanyId = x.CurrentCompanyId,
                IntCurrentFinancialYear = x.IntCurrentFinancialYear,
                IsActive = x.IsActive,
                Staff_To_User_Rate_Per_Second = x.Staff_To_User_Rate_Per_Second,
                one_paisa_to_coin_rate = x.one_paisa_to_coin_rate
            }).ToList();
        }
        public async Task<AppMasterSettingDTO?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);

            if (item == null) return null;

            return new AppMasterSettingDTO
            {
                AppMasterSettingId = item.AppMasterSettingId,
                CurrentCompanyId = item.CurrentCompanyId,
                IntCurrentFinancialYear = item.IntCurrentFinancialYear,
                IsActive = item.IsActive,
                Staff_To_User_Rate_Per_Second = item.Staff_To_User_Rate_Per_Second,
                one_paisa_to_coin_rate = item.one_paisa_to_coin_rate
            };
        }

        public async Task<bool> UpdateAsync(AppMasterSetting req)
        {
            var old = await _repo.GetByIdAsync(req.AppMasterSettingId);
            if (old == null) return false;

            _repo.Detach(old);
            _repo.Update(req);

            await _repo.SaveChangesAsync();
            return true;
        }
    }
}

