using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using MediatR;
namespace Hospital.Application.Features.Pharmacist.Command
{
    public class DeletePharmacistCommandHandler : IRequestHandler<DeletePharmacistCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePharmacistCommandHandler(IUnitOfWork  unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Unit> Handle(DeletePharmacistCommand request, CancellationToken cancellationToken)
        {
            var pharmacist = await _unitOfWork.Pharmacists.GetByIdAsync(request.Id);
            if (pharmacist == null)
            {
                throw new Exception("Pharmacist not found");
            }

            await _unitOfWork.Pharmacists.DeleteAsync(pharmacist);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
