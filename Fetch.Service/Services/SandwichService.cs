using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Fetch.Shared;
using Google.Protobuf.Collections;
using Fetch.Service.Data;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using static System.Formats.Asn1.AsnWriter;
using Fetch.Model.Entities;

namespace Fetch.Service.Services
{

    public class SandwichService : SandwichShopService.SandwichShopServiceBase
    {
        private readonly HttpClient _httpClient;
        private readonly FetchContext _fetchDbContext;
        public SandwichService(FetchContext fetchDbContext)
        {
            _httpClient = new HttpClient();
            _fetchDbContext = fetchDbContext;
        }

        /// <summary>
        /// Get All Sandwiches Shops in North Carolina, Tri Area.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>A list of all sandwich shops</returns>
        public override async Task<GetAllSandwichShopsResponse> GetAllSandwichShops(Empty request, ServerCallContext context)
        {

            RepeatedField<SandwhichLocation> locations = new();
            var responseObj = new GetAllSandwichShopsResponse();

            try
            {

                var sandwichShops = await _fetchDbContext.Shops
                             .Include(shop => shop.Locations)
                             .ToListAsync();

                foreach (var shop in sandwichShops)
                {
                   
                    var protoShop = new SandwichShop
                    {
                        Id = shop.Id,
                        Shop = shop.Shop1,
                        Specialty = shop.Specialty,
                    };

                    // Add location data
                    foreach (var location in shop.Locations)
                    {
                        var protoLocation = new SandwhichLocation
                        {
                            Id = location.Id,
                            ShopId = shop.Id, // or location.ShopId if it's available
                            Address = location.Address
                        };
                        protoShop.Address.Add(protoLocation); // Add each location to the shop
                    }

                    responseObj.SandwichShops.Add(protoShop);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sandwich shops: {ex.Message}");
            }

            return responseObj;
        }

        public override Task<SandwichShopResponse> CreateSandwichShop(CreateSandwichShopRequest request, ServerCallContext context)
        {
            return base.CreateSandwichShop(request, context);
        }

        public override Task<Empty> DeleteSandwichShop(DeleteSandwichShopRequest request, ServerCallContext context)
        {
            return base.DeleteSandwichShop(request, context);
        }

        public override Task<SandwichShopResponse> GetSandwichShop(GetSandwichShopRequest request, ServerCallContext context)
        {
            return base.GetSandwichShop(request, context);
        }

        public override Task<SandwichShopResponse> UpdateSandwichShop(UpdateSandwichShopRequest request, ServerCallContext context)
        {
            return base.UpdateSandwichShop(request, context);
        }
    }
}
