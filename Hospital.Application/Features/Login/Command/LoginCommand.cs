using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Login.Command
{
    public class LoginCommand : IRequest<LoginResponce>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
