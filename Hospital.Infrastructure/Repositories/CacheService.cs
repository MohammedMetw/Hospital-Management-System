using Hospital.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Hospital.Infrastructure.Repositories
{
  public  class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task BlacklistTokenAsync(string token)
        {
            
            await _database.StringSetAsync(token, "blacklisted", TimeSpan.FromHours(1));
        }
        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            
            return await _database.KeyExistsAsync(token);
        }

    }
}
