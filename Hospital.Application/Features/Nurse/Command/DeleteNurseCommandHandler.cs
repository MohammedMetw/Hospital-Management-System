using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Nurse.Command
{
  public  class DeleteNurseCommandHandler : IRequestHandler<DeleteNurseCommand,NurseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteNurseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<NurseDto> Handle(DeleteNurseCommand request, CancellationToken cancellationToken)
        {
            var nurse = await _unitOfWork.Nurses.GetByIdAsync(request.Id);
            if (nurse == null)
            {
                throw new Exception("Nurse not found");
            }
           await _unitOfWork.Nurses.DeleteAsync(nurse);
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