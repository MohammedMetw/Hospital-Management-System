using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;


namespace Hospital.Application.Features.Nurse.Command
{
   public class DeleteNurseCommand : IRequest<NurseDto>
    {
        public int Id { get; set; }
        public DeleteNurseCommand(int id)
        {
            Id = id;
        }
    }
}
