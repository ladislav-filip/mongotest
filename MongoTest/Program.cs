using MongoTest;

var serviceProvider = ServiceProviderHelper.ConfigureServices();

var srv = serviceProvider.GetRequiredService<UzivateleService>();
//srv.EraseAndFill();
// srv.ListSimpleByJmeno("Ladislav");
// srv.ListKuraciNad20();
//srv.ListRidiciAB();
//srv.PocetJmen();
//srv.ProjekceAsync().Wait();
//srv.ProjekceDve();
//srv.UpdatePribor();

Console.WriteLine("Finnish.");
Console.ReadKey();