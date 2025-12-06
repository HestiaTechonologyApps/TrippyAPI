using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class AppMasterSettingRepository : GenericRepository<AppMasterSetting>, IAppMasterSettingRepository
    {
        private readonly AppDbContext _context;
        public AppMasterSettingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddAsync(AppMasterSetting setting)
        {
            await _context.AppMasterSettings.AddAsync(setting);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AppMasterSetting>> GetAllAsync()
        {
            return await _context.AppMasterSettings.ToListAsync();
        }

        public async Task<AppMasterSetting> FindFirstAsync()
        {
            return await _context.AppMasterSettings.Where(u => u.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<AppMasterSetting> GetByIdAsync(int logId)
        {
            return await _context.AppMasterSettings.FindAsync(logId);
        }
    }
}
