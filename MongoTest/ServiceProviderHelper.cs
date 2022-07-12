namespace MongoTest;

public static class ServiceProviderHelper
{
    public static ServiceProvider ConfigureServices()
    {
        var collection = new ServiceCollection();

        collection.AddLogging(conf =>
        {
            conf.SetMinimumLevel(LogLevel.Debug);
            conf.AddConsole();
        });

        collection.AddSingleton<MongoClientFactory>(provider =>
        {
            var logger = provider.GetService<ILogger<MongoClientFactory>>();
            return new MongoClientFactory(logger, "mongodb://root:lok@127.0.0.1:27017");
        });
            
        collection.AddSingleton<IMongoClient>(provider => provider.GetRequiredService<MongoClientFactory>().Create());
        collection.AddSingleton<IMongoDatabase>(provider =>
        {
            var client = provider.GetService<IMongoClient>();
            var db = client.GetDatabase("pilifs");
            return db;
        });
        collection.AddTransient<UzivateleService>();
        collection.AddTransient<ISequenceRepository, SequenceRepository>();

        var serviceProvider = collection.BuildServiceProvider();
        return serviceProvider;
    }
}