using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Application.Features.Dashboard.Queries
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetDashboardStatsQueryHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var stats = new DashboardStatsDto
            {
                TotalUsers = await _userManager.Users.CountAsync(cancellationToken),
                TotalDoctors = await _unitOfWork.Doctors.CountAsync(),
                TotalPatients = await _unitOfWork.Patients.CountAsync(),
                TotalPharmacists = await _unitOfWork.Pharmacists.CountAsync(),
                TotalDepartments = await _unitOfWork.Departments.CountAsync()
            };

            return stats;
        }
    }
}
