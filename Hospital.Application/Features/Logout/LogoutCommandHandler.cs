using Hospital.Application.Features.Logout;
using Hospital.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
{
    private readonly ICacheService _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogoutCommandHandler(ICacheService cache, IHttpContextAccessor httpContextAccessor)
    {
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"]
            .ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token))
        {
            await _cache.BlacklistTokenAsync(token);
        }

        return Unit.Value;
    }
}