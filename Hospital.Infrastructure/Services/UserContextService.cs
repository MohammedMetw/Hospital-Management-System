using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? GetUser() => _httpContextAccessor.HttpContext?.User;

    public string? GetUserId() => GetUser()?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string? GetUserRole() => GetUser()?.FindFirst(ClaimTypes.Role)?.Value;
}