using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.Nurse.Command
{
  public  class DeleteNurseCommandHandler : IRequestHandler<DeleteNurseCommand,NurseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteNurseCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<NurseDto> Handle(DeleteNurseCommand request, CancellationToken cancellationToken)
        {
            var nurse = await _unitOfWork.Nurses.GetByIdAsync(request.Id);
            if (nurse == null)
            {
                throw new Exception("Nurse not found");
            }
           await _unitOfWork.Nurses.DeleteAsync(nurse);
            await _userManager.RemoveFromRoleAsync(nurse.ApplicationUser, "Nurse");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new NurseDto
            {
                Id = nurse.Id,
                FullName = $"{nurse.ApplicationUser.FirstName} {nurse.ApplicationUser.LastName}",
                DepartmentId = nurse.DepartmentId,
            };
        }
    }
    

    }