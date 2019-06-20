using MongoDB.Bson;

namespace MongoTest.Models
{
    public class UzivatelBase
    {
        public ObjectId Id { get; set; }

        public string Jmeno { get; set; }

        public string Prijmeni { get; set; }
    }

    public class Uzivatel<TVlastnost> : UzivatelBase 
        where TVlastnost : IVlastnost
    {
        public TVlastnost Data { get; set; }
    }

    //public class UzivatelRidic : Uzivatel<VlastnostRidic> { }

    public interface IVlastnost
    {
        // marked interface
    }

    public class VlastnostRidic : IVlastnost
    {
        public string[] Skupiny { get; set; }

        public int RokVydani { get; set; }
    }

    public class VlastnostKurak : IVlastnost
    {
        public int PocetDenne { get; set; }

        public string OblibenaZnacka { get; set; }
    }
}
