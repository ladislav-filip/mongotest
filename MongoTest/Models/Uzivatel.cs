namespace MongoTest.Models
{
    public class Uzivatel<TVlastnost> : UzivatelBase 
        where TVlastnost : IVlastnost
    {
        public TVlastnost Data { get; set; }
    }

    //public class UzivatelRidic : Uzivatel<VlastnostRidic> { }
}
