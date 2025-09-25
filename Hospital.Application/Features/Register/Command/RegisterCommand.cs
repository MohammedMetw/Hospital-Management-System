using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;

using MediatR;

namespace Hospital.Application.Features.Register.Command
{
   public class RegisterCommand : IRequest<RegisterUserResponse>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }


    }
}
