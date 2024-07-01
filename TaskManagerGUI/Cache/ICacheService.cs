using Microsoft.Extensions.Caching.Memory;

namespace TaskManagerGUI.Cache
{
    public interface ICacheService
    {
        IMemoryCache MemoryCache { get; set; }

        object Get(string key);
        void Set(string key, object value);
    }
}