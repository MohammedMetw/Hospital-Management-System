using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.User.Command
{
    public class lockUserCommandHandler : IRequestHandler<lockUserCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public lockUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(lockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new Exception ("User not found");
            }

            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            return Unit.Value;
        }
    }
}
