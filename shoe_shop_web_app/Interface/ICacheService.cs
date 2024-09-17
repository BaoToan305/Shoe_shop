using Microsoft.Extensions.Caching.Memory;

namespace shoe_shop_web_app.Interface
{
    public interface ICacheService
    {
        T Get<T>(string cacheKey) where T : class;
        void Set(string cacheKey, object item, int minutes);
    }

}
