using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Models
{
    public class User
    {   [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(120)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string EmailId{get;set;}
        [Required]
        [MaxLength(20)]
        public string UserPassword { get; set; }

        
    }
}
