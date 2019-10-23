﻿using System.ComponentModel.DataAnnotations;

namespace MODEL.Models
{
    public class AdminUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }

        public bool SuperAdmin { get; set; }
    }
}