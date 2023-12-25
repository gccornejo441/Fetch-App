using Fetch.Model.Entities;
using Fetch.Service.Data;
using Microsoft.EntityFrameworkCore;

namespace Fetch.Service.TestServices
{
    public class ShopService
    {
        private readonly FetchContext _fetchDbContext;
        public ShopService(FetchContext fetchDbContext) 
        {
            _fetchDbContext = fetchDbContext;
        }

        public List<Shop> SandwichShops { get; private set; } = new();

        public async Task GetShops()
        {
            SandwichShops = await _fetchDbContext.Shops.ToListAsync();
        }
    }
}
