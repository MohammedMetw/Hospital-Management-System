using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Accountant.Queries
{
   public class GetAccountantByIDQuery : IRequest<AccountantDto>
    {
        public int Id { get; set; }
        public GetAccountantByIDQuery(int id)
        {
            Id = id;
        }
    }
}
