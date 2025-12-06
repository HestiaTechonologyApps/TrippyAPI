using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IAppMasterSettingService
    {
        Task<List<AppMasterSettingDTO>> GetAllAsync();
        Task<AppMasterSettingDTO?> GetByIdAsync(int id);
        Task<AppMasterSettingDTO> CreateAsync(AppMasterSetting req);
        Task<bool> UpdateAsync(AppMasterSetting req);
        Task<bool> DeleteAsync(int id);
    }
}
