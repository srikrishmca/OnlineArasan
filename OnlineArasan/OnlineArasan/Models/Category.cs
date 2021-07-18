using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineArasan.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }

        [Required(ErrorMessage = "Please choose category image")]
        [Display(Name = "Profile Picture")]
        public IFormFile CategoryImageFile { get; set; }
    }

    
}
