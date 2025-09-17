using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.User.Command
{
    public class UnlockUserCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
