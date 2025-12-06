using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.Entities
{
    public class AppMasterSetting
    {
        [Key]
        public int AppMasterSettingId { get; set; }
        public int CurrentCompanyId { get; set; }
        public int IntCurrentFinancialYear { get; set; } = 1;

        public bool IsActive { get; set; } = true;
        public decimal Staff_To_User_Rate_Per_Second { get; set; } = 50m;
        public decimal one_paisa_to_coin_rate { get; set; } = 6;
    }
}
