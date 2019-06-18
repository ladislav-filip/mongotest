using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace MongoTest
{
    class Program
    {
        static IMongoDatabase db;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dbClient = new MongoClient("mongodb://root:example@127.0.0.1:27017");

            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases are :");
            foreach (var item in dbList)
            {
                Console.WriteLine(item);
            }

            db = dbClient.GetDatabase("pilifs");

            var collList = db.ListCollections().ToList();
            if (collList.Count == 0)
            {
                InitDb();
            }
            InitDb();

            Console.WriteLine("The list of collections are :");
            foreach (var item in collList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Finnish.");
            Console.ReadKey();
        }

        static void InitDb()
        {
            var uzivatele = db.GetCollection<Uzivatel>("uzivatele");
            var id = ObjectId.GenerateNewId();

            var item = new Uzivatel()
            {
                Id = 1,
                Jmeno = "Ladislav",
                Prijmeni = "Filip"
            };

            uzivatele.InsertOne(item);
        }

        static void VymazatUzivatele()
        {
            var uzivatele = db.GetCollection<Uzivatel>("uzivatele");
            FilterDefinition<Uzivatel> filter = "{ Jmeno: \"Ladislav\" }";
            uzivatele.DeleteMany(filter);
        }

    }

    class Uzivatel
    {
        public int Id { get; set; }

        public string Jmeno { get; set; }

        public string Prijmeni { get; set; }
    }
}
