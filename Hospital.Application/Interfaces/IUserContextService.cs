using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

public interface IUserContextService
{
    
    ClaimsPrincipal? GetUser();

    string? GetUserId();
    string? GetUserRole();
}