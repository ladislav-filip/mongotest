using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoTest.Services;
using System;

namespace MongoTest
{
    class Program
    {
        static IMongoDatabase db;

        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            Console.WriteLine("Hello World!");

            var srv = serviceProvider.GetService<UzivateleService>();
            srv.EraseAndFill();
            srv.ListSimpleByJmeno("Ladislav");
            srv.ListKuraciNad20();

            Console.WriteLine("Finnish.");
            Console.ReadKey();
        }

        private static ServiceProvider ConfigureServices()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IMongoClient>(provider => new MongoClient("mongodb://root:example@127.0.0.1:27017"));
            collection.AddSingleton<IMongoDatabase>(provider =>
            {
                var client = provider.GetService<IMongoClient>();
                db = client.GetDatabase("pilifs");
                return db;
            });
            collection.AddTransient<UzivateleService>();

            var serviceProvider = collection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
