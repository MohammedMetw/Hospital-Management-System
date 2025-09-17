using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.User.Command
{
    public class AssignRoleToUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public string RoleName { get; set; }
    }
}
