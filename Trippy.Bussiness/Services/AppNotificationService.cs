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
   

    public class AppNotificationService        :         IAppNotificationService
    {
        private readonly IAppNotificationRepository _repo;
        public AppNotificationService(IAppNotificationRepository repo)
        {
            _repo = repo;
        }

        public Task<AppNotification> CreateAsync(AppNotification coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppNotification>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppNotification?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AppNotification coupon)
        {
            throw new NotImplementedException();
        }
    }

}
