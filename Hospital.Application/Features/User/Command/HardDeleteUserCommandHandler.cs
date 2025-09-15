
using MediatR;
using Microsoft.AspNetCore.Identity;
using Hospital.Domain.Entities;
using Hospital.Application.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hospital.Application.Features.User.Command;

public class HardDeleteUserCommandHandler : IRequestHandler<HardDeleteUserCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public HardDeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(HardDeleteUserCommand request, CancellationToken cancellationToken)
    {
        
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException($"User with ID '{request.UserId}' was not found.");
        }

       
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to delete user. " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

       
        return Unit.Value;
    }
}