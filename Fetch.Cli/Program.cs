using Fetch.Shared;
using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fetch.Cli;

public class Program
{
    static async Task Main(string[] args)
    {

        var channel = GrpcChannel.ForAddress("https://localhost:7290");
        var client = new SandwichShopService.SandwichShopServiceClient(channel);

        var shops = client.GetAllSandwichShops(new Google.Protobuf.WellKnownTypes.Empty());

        var sandwichShops = shops.SandwichShops;

        if (shops != null)
        {
            foreach (var shop in sandwichShops)
            {
                foreach (var location in shop.Address)
                {
                    Console.WriteLine($"Shop Id: {shop.Id}{Environment.NewLine}Shop Name: {shop.Shop}{Environment.NewLine}Addresses:");
                    Console.WriteLine($" - Id: {location.Id}, ShopId: {shop.Id}, Address: {location.Address}");
                }
            }
        }
        else
        {
            Console.WriteLine("no shops");
        }
            //         Console.WriteLine($" - Id: {location.Id}, ShopId: {shop.Id}, Address: {location.Address}");
            //    Console.WriteLine($"Shop Id: {shop.Id}{Environment.NewLine}Shop Name: {shop.Shop}{Environment.NewLine}Addresses:");

            Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}
