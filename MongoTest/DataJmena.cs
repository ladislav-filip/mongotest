namespace MongoTest
{
    internal static class DataJmena
    {
        static string Raw = @"Pavel Zvonař
Matěj Jirsa
Jitka Dřevojanová
Jaroslav Růzha
Oldřich Bílek
Štěpánka Krajíčková
Irena Hartychová
Mihail Brož
František Jeřábek
Vlasta Pěkná
Simona Červená
Ilona Pobudová
Olga Neubauerová
Martina Petrášová
Miloslava Haaseová
Karolína Jurková
Marcela Winkelhöferová
Martin Hronek
Růžena Stuchlíková
Hana Otřísalová
Jiří Polák
Vladko Širaj
Jan Kummer
Marie Kovačková
Rostislav Krichfalovshiy
Nadiya Bolcková
Jaroslava Kešelová
Eliška Chroustová
Vladimír Karásek
Ladislav Pavlíček
Milan Petrus
Ján Kec
Viktor Nypl
Michaela Popovičová
Josef Gold
Orlin Strahilov Popek
Frederika Želivská
Zdeněk Šimek
Petr Hrabec
Jana Horká
Michal Průcha
Alena Bartůňková
Jakub Rezler
Věra Mrázová
Jindřich Kopecký
Ivana Podzimková
Eva Schneiderová
Markéta Heřmanová
Gabriela Zánová
Zdeňka Šedivá
Bohuslav Tošner
Ludmila Sedláčková
Rudolf Sterzl
Blanka Švorcová
Ludvík Macháček
Radoslava Bažantová
Daniel Rotter
Anna Šťastná
Miroslav Dědek
Jaromír Mitrenga
Tereza Fořstová
Kateřina Dudková
Antonín Tarkay
Václav Dunovský
Tomáš Gelnar
Monika Markvartová
David Havránek
Lenka Králová
Samuel Znamínko
Milada Sovová
Veronika Gasiorová
Iveta Tručková
Roman Čermák
Lidmila Koudelková
Jarmila Turinová
Karel Samec
Helena Fiedlerová
Lucie Fleischmannová
Edita Balošáková
Julius Trešl
Hedvika Svobodová
Tadeáš Charvát
Barbora Křížová
Sabina Hoďová
Petra Rudovská
Vendula Richterová
Soňa Spielmannová
Bohumil Nýdrle
Dominik Lupienski
Vladislav Havlík
Lukáš Blaška
Božena Gröbnerová
Pavla Hudečková
Krista Kamenova
Růženka Dvořáková
Miroslava Hladíková
Miloslav Markvart
Lubomír Svoboda
Anežka Draštíková
Marta Pánková
Jana Francová
Antonio William Le
Veronika Hladíková
Václav Rudolf
Marie Humplíková
Kateřina Pešková
Ondřej Láník
František Hrychka
Vladislava Brdová
Bohumil Háb
Miroslava Jelínková
David Trübenekr
Petr Kohout
Jarmila Černá
Sabina Růžičková
Vlasta Godulová
Andriy Bargel
Josef Knotek
Hana Grézlová
Ivo Kučera
Jiří Cihlář
Radim Hrdlička
Gennadiy Laidorf
Rudolf Marek
Pavel Paďourek
Jan Ďuriš
Viktor Kedroň
Monika Bělovská
Jakub Krpec
Gabriela Warthová
Zbyněk Bartoš
Andrea Štinglová
Marta Moláčková
Milena Kovářová
Milan Věříš
Lydie Oborná
Michal Gal
Radka Hamašová
Pavlína Hamerlová
Miroslav Šmerda
Aleš Myšička
Tomáš Macourek
Jaroslav Hovorka
Karel Cejp
Mohamed Samir Hlaváč
Věra Bauerová
Eduard Čákora
Jiřina Novosadová
Martin Trnka
Michaela Marysková
Eva Cejhonová
Vlastimil Sklenák
Luděk Kobos
Martina Břoušková
Leonhard Jin
Dana Prokešová
Šárka Brabcová
Anna Pavolová
Daniel Franzl
Zdeněk Holý
Ludmila Friebelová
Marcela Kokešová
Alena Hybnerová
Ivana Velvarská
Trung Thanh Jelen
Růžena Dorňáková
Klára Pavlíková
Lenka Václaviková
Božena Korčáková
Alexandra Velčovská
Lucie Šimečková
Renáta Palečková
Ladislav Malcát
Libuše Předotová
Vladimír Kapoun
Zdeňka Hejzková
Iveta Jonová
Jolana Hořáková
Andrian Šlejtr
Adéla Zuranová
Petra Dosedělová
Arnold Dušek
Jaroslava Cinková
Roman Polášek
Jitka Procházková
Stanislava Doubravová
Lukáš Koudelka
Kristýna Adámková
Helena Havrdová
Irena Vávrová
Michael Povalač
Oldřiška Pelikánová
Stanislav Voříšek
Robert Novák
Oldřich Zhmurko
Dagmar Říhová
Richard Rendl
Tereza Bazínková
Markéta Míšková
Daniela Zborovská";

        static string[] Obce = new string[] { "Příbor", "Ostrava", "Praha", "Brno", "Olomouc", "Louny", "Krmelín" };

        public static IEnumerable<UzivatelBase> GetJmena()
        {
            var rnd = new Random();
            var result = new List<UzivatelBase>();
            foreach(var line in Raw.Split(Environment.NewLine))
            {
                var tmp = line.Split(' ');
                var uziv = new UzivatelBase()
                {
                    Jmeno = tmp[0],
                    Prijmeni = tmp[1],
                    Obec = Obce[rnd.Next(Obce.Length - 1)],
                    TrvaleBydliste = rnd.Next(5) > 2
                };
                result.Add(uziv);
            }
            return result;
        }

    }
}
