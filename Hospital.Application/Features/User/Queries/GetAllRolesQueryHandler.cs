using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Application.Features.User.Queries
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<string>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles
                .Select(r => r.Name) 
                .ToList();

            return roles;
        }
    }
}
