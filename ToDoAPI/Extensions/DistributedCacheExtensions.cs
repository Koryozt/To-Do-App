using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace ToDoAPI.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache Cache, 
                                                   string RecordID, 
                                                   T Data, 
                                                   TimeSpan? AbsoluteExpiredTime = null, 
                                                   TimeSpan? UnusedExpiredTime = null)
        {
            DistributedCacheEntryOptions Options = new();

            Options.AbsoluteExpirationRelativeToNow = AbsoluteExpiredTime ?? TimeSpan.FromSeconds(60);
            Options.SlidingExpiration = UnusedExpiredTime;

            string JsonData = JsonSerializer.Serialize(Data);

            await Cache.SetStringAsync(RecordID, JsonData, Options);
        }

        public static async Task<T?> GetRecordAsync<T>(this IDistributedCache Cache, string RecordID)
        {
            string JsonData = await Cache.GetStringAsync(RecordID);

            if (JsonData is null)
                return default(T);
            
            return JsonSerializer.Deserialize<T>(JsonData);
        }
    }
}