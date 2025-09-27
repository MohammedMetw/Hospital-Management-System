using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.Login.Command
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string Token { get; set; }
    }
}
