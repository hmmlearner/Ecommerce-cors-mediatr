using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.UnitTests
{
    [SetUpFixture]
    public partial class Testing
    {

        private static ITestDatabase _database;
        private static CatalogueWebApplicationFactory _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
           // _database = await TestDatabaseFactory.CreateAsync();

           // _factory = new CustomWebApplicationFactory(_database.GetConnection());

            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task SendAsync(IBaseRequest request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            await mediator.Send(request);
        }
    }
}
