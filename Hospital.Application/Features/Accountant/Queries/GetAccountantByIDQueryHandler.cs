using System.Threading;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Application.DTOs;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Features.Accountant.Queries
{
    public class GetAccountantByIDQueryHandler : IRequestHandler<GetAccountantByIDQuery, AccountantDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountantByIDQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountantDto> Handle(GetAccountantByIDQuery request, CancellationToken cancellationToken)
        {
            var accountant = await _unitOfWork.Accountants.GetByIdAsync(request.Id);

            if (accountant == null)
            {
                return null;
            }

            var accountantDto = new AccountantDto
            {
                Id = accountant.Id,
                FullName = $"{accountant.ApplicationUser.FirstName} {accountant.ApplicationUser.LastName}",
                Email = accountant.Email,
                PhoneNumber = accountant.PhoneNumber
                
            };

            return accountantDto;
        }
    }
}
