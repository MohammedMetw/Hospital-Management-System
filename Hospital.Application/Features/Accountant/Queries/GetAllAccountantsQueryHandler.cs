using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Accountant.Queries
{
  public  class GetAllAccountantsQueryHandler : IRequestHandler<GetAllAccountantsQuery, IEnumerable<AccountantDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllAccountantsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<AccountantDto>> Handle(GetAllAccountantsQuery request, CancellationToken cancellationToken)
        {
            var accountants = await _unitOfWork.Accountants.GetAllAsync();
            var accountantDtos = accountants.Select(accountant => new AccountantDto
            {
                Id = accountant.Id,
                FullName = $"{accountant.ApplicationUser.FirstName} {accountant.ApplicationUser.LastName}",
                Email = accountant.ApplicationUser.Email,
                PhoneNumber = accountant.ApplicationUser.PhoneNumber
               
            }).ToList();

            return accountantDtos; 
        }
    }
}
