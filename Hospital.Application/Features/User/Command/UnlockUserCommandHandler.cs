using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Exceptions;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.User.Command
{
    public class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand,Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UnlockUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Unit> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            return Unit.Value;
        }
    }
}
