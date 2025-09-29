using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    class SettingsDTO
    {
    }
    public class LanguageDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class InterestDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
    public class GiftDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Coin { get; set; }
    }
}
