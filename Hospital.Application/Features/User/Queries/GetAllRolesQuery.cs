using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.User.Queries
{
    public class GetAllRolesQuery : IRequest<IEnumerable<string>>
    {
    }
}
