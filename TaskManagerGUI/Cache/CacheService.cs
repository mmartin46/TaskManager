using Microsoft.Extensions.Caching.Memory;

namespace TaskManagerGUI.Cache
{
    public class CacheService : ICacheService
    {
        public IMemoryCache MemoryCache { get; set; }
        public CacheService(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public object Get(string key)
        {
            object value = MemoryCache.Get(key) ?? string.Empty;
            if (value.Equals(string.Empty))
            {
                return "Unknown message";
            }
            return value;
        }

        public void Set(string key, object value)
        {
            MemoryCache.Set(key, value, TimeSpan.FromMinutes(1));
        }
    }
}
