using Hospital.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.ChangePassword
{
    public class ChangePasswordCommand:IRequest<bool>
    {
       
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
