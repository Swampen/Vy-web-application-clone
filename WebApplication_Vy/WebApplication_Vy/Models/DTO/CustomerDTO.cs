﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO
{

    public class CustomerDTO
    {
        public int Id { get; set; }

        [Display(Name = "Given name")]
        [Required(ErrorMessage = "This field is required")]
        public string Givenname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public ZipcodeDTO Zipcode { get; set; }
    }
}