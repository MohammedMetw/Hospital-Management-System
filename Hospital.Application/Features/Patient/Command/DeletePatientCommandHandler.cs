using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hospital.Application.Features.Patient.Command
{
    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand , Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeletePatientCommandHandler(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public async Task<Unit> Handle (DeletePatientCommand command , CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(command.Id);
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }
            await _unitOfWork.Patients.DeleteAsync(patient);
            await _userManager.RemoveFromRoleAsync(patient.ApplicationUser, "Patient");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
