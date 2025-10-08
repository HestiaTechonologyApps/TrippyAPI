﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRIPPY.DOMAIN.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Address { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public bool Islocked { get; set; } = false;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? Lastlogin { get; set; }
        

    }
}
