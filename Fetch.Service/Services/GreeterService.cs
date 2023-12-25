using Fetch.Service;
using Fetch.Shared;
using Grpc.Core;
using System.Net.Http;

namespace Fetch.Service.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        HttpClient _httpClient;

        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
