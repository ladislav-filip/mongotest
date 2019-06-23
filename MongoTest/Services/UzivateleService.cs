﻿using MongoDB.Bson;
using MongoDB.Driver;
using MongoTest.DAL;
using MongoTest.Models;
using System;
using System.Linq;

namespace MongoTest.Services
{
    public class UzivateleService : BaseService<Uzivatel<IVlastnost>>
    {
        private readonly ISequenceRepository m_sequenceRepository;

        public UzivateleService(IMongoDatabase database, ISequenceRepository sequenceRepository) : base(database)
        {
            m_sequenceRepository = sequenceRepository;
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

        public void PocetJmen()
        {
            var coll = _database.GetCollection<BsonDocument>(GetCollectionName());
            var aggregate = coll.Aggregate().Group(new BsonDocument { { "_id", "$Jmeno" }, { "count", new BsonDocument("$sum", 1) } });

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }
        }

        public async System.Threading.Tasks.Task ProjekceAsync()
        {
            var coll = Collection;
            var list = await coll.Find(p => p.Jmeno == "Petr").Project(p => new { CeleJmeno = $"{p.Prijmeni} {p.Jmeno}", Inicialy = p.Prijmeni.First().ToString() + "" + p.Jmeno.First().ToString() }).ToListAsync();
            foreach(var d in list)
            {
                Console.WriteLine(d);
            }
        }

        public void ProjekceDve()
        {
            FieldDefinition<BsonDocument> field = "Obec";
            var projection = Builders<BsonDocument>.Projection.Include(field);
            var list = _database.GetCollection<BsonDocument>(GetCollectionName()).Find(new BsonDocument("Jmeno", "Pavel")).Project(projection).ToList();
            foreach (var d in list)
            {
                Console.WriteLine(d);
            }
        }

        public void UpdatePribor()
        {
            var coll = _database.GetCollection<BsonDocument>(GetCollectionName());
            FieldDefinition<BsonDocument> field = "Obec";
            var projection = Builders<BsonDocument>.Projection.Include(field);
            var list = coll.Find(new BsonDocument("Obec", "Příbor")).Project(projection).ToList();
            foreach (var d in list)
            {
                Console.WriteLine(d["_id"]);
                coll.FindOneAndUpdate(Builders<BsonDocument>.Filter.Eq("_id", d["_id"]),
                    Builders<BsonDocument>.Update.Set("Jmeno", "aaaa"));
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
                    Id = m_sequenceRepository.GetSequenceValue(GetCollectionName()),
                    Jmeno = d.Jmeno,
                    Prijmeni = d.Prijmeni,
                    Obec = d.Obec,
                    TrvaleBydliste = d.TrvaleBydliste,
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
                    Id = m_sequenceRepository.GetSequenceValue(GetCollectionName()),
                    Jmeno = d.Jmeno,
                    Prijmeni = d.Prijmeni,
                    Obec = d.Obec,
                    TrvaleBydliste = d.TrvaleBydliste,
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
