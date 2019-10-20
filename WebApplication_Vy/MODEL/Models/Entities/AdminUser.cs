﻿using System.ComponentModel.DataAnnotations;

namespace MODEL.Models
{
    public class AdminUser
    {
        [Key]
        public string UserName { get; set; }
        
        public int Id { get; set; }
        
        public string Password { get; set; }

        public bool SuperAdmin { get; }
    }
}