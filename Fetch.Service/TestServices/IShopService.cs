using Fetch.Model.Entities;

namespace Fetch.Service.TestServices
{
    public interface IShopService
    {
        public Task<List<Shop>> GetShops();
    }
}
