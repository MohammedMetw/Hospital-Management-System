using MediatR;
using Hospital.Application.DTOs;
using System.Collections.Generic;

namespace Hospital.Application.Features.User.Queries
{

    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {

    }
}