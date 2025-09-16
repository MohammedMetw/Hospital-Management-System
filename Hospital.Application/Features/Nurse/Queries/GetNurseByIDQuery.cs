using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;


namespace Hospital.Application.Features.Nurse.Queries
{
    public class GetNurseByIDQuery : IRequest<NurseDto>
    {
        public int Id { get; set; }
        public GetNurseByIDQuery(int id)
        {
            Id = id;
        }
    }
}
