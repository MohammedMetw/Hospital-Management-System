using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.ChangePassword
{
   public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand,bool>
    {
        private readonly IUserContextService _userContextService;
         
        private readonly UserManager<ApplicationUser> _usermanger;

        public ChangePasswordCommandHandler(IUserContextService userContextService , UserManager<ApplicationUser>userManager) 
        {
            _userContextService = userContextService;
           
            _usermanger = userManager;
        }
        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var user = await _usermanger.FindByIdAsync(userId);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            await _usermanger.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            return true;

        }

    }
}
