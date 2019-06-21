using MongoDB.Bson;
using MongoDB.Driver;
using MongoTest.Models;
using System;
using System.Linq;

namespace MongoTest.Services
{
    public class UzivateleService : BaseService<Uzivatel<IVlastnost>>
    {
        public UzivateleService(IMongoDatabase database) : base(database)
        {
        }

        public void Erase()
        {
            Collection.DeleteMany(Builders<Uzivatel<IVlastnost>>.Filter.Where(p => !string.IsNullOrEmpty(p.Jmeno)));
        }


        public void EraseAndFill()
        {
            Erase();

            FillRidici();
            FillKuraci();
        }

        public void ListSimpleByJmeno(string jmeno)
        {
            var data = Collection.Find(Builders<Uzivatel<IVlastnost>>.Filter.Where(p => p.Jmeno == jmeno)).ToList();

            foreach(var d in data)
            {
                Console.WriteLine($"{d.Jmeno} {d.Prijmeni}");
            }
        }

        public void ListKuraciNad20()
        {
            var collectionMy = _database.GetCollection<Uzivatel<VlastnostKurak>>(GetCollectionName());
            var data = collectionMy.Find(Builders<Uzivatel<VlastnostKurak>>.Filter.Where(p => p.Data.PocetDenne > 20)).ToList();

            foreach (var d in data)
            {
                Console.WriteLine($"{d.Jmeno} {d.Prijmeni}, počet = {d.Data.PocetDenne}");
            }
        }

        private void FillKuraci()
        {
            var znacky = new string[] { "Start", "Mars", "Sparta", "Marlboro", "ViceRoy" };
            var rnd = new Random();
            var data = DataJmena.GetJmena().Take(50);
            foreach (var d in data)
            {
                var item = new Uzivatel<IVlastnost>()
                {
                    Id = ObjectId.GenerateNewId(),
                    Jmeno = d.Jmeno,
                    Prijmeni = d.Prijmeni,
                    Data = new VlastnostKurak()
                    {
                        PocetDenne = rnd.Next(100),
                        OblibenaZnacka = znacky[rnd.Next(znacky.Length - 1)]
                    }
                };
                Collection.InsertOne(item);
            }
        }

        private void FillRidici()
        {
            var rnd = new Random();
            var skupiny = new string[] { "A", "B", "C", "D", "E" };
            var data = DataJmena.GetJmena().Skip(50);
            foreach (var d in data)
            {
                var item = new Uzivatel<IVlastnost>()
                {
                    Id = ObjectId.GenerateNewId(),
                    Jmeno = d.Jmeno,
                    Prijmeni = d.Prijmeni,
                    Data = new VlastnostRidic()
                    {
                        Skupiny = new string[] { skupiny[rnd.Next(skupiny.Length - 1)], skupiny[rnd.Next(skupiny.Length - 1)] },
                        RokVydani = rnd.Next(1970, 2019)
                    }
                };
                Collection.InsertOne(item);
            }
        }
    }


}
