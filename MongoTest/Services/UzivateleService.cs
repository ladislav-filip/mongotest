using MongoDB.Bson;
using MongoDB.Driver;
using MongoTest.Models;
using System;

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
            var data = new string[] { "Miloš,Zeman,40,Mars", "Vaclav,Klaus,5,Start", "Miloš,Forman,12,Mars", "Martin,Dejdar,22,Sparta" };
            foreach (var d in data)
            {
                var di = d.Split(',');
                var item = new Uzivatel<IVlastnost>()
                {
                    Id = ObjectId.GenerateNewId(),
                    Jmeno = di[0],
                    Prijmeni = di[1],
                    Data = new VlastnostKurak()
                    {
                        PocetDenne = int.Parse(di[2]),
                        OblibenaZnacka = di[3]
                    }
                };
                Collection.InsertOne(item);
            }
        }

        private void FillRidici()
        {
            var data = new string[] { "Ladislav,Filip,A-B", "Petr,Kopec,C-D", "Jan,Duchacek,E", "Ladislav,Pohrobek,E" };
            foreach (var d in data)
            {
                var di = d.Split(',');
                var item = new Uzivatel<IVlastnost>()
                {
                    Id = ObjectId.GenerateNewId(),
                    Jmeno = di[0],
                    Prijmeni = di[1],
                    Data = new VlastnostRidic()
                    {
                        Skupiny = di[2].Split('-')
                    }
                };
                Collection.InsertOne(item);
            }
        }
    }


}
