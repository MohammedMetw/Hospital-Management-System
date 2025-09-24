using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;

namespace Hospital.Application.DTOs
{
    public class LoginResponce
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
       
        public required string Token { get; set; }
        public required IList<string> Roles { get; set; }
        //public DateTime ExpiresOn { get; set; }
    }
}
