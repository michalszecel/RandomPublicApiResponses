using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using RandomPublicApiResponses.Models;
using RandomPublicApiResponses.Repositories;

[assembly: FunctionsStartup(typeof(RandomPublicApiResponses.Startup))]

namespace RandomPublicApiResponses;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

        // to discuss if such repository should not be Singleton
        // imho should be so that the instance can be reused and not created every time - saving time on object creatiopn
        // also would be good to automate this part by for example using reflection to register all repositories
        builder.Services.AddTransient<IGenericDataTableRepository<RandomApiResponseModel>, GenericDataTableRepository<RandomApiResponseModel>>();
        builder.Services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();
    }
}