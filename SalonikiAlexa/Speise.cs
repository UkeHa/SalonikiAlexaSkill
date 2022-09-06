using CsvHelper.Configuration;

namespace SalonikiAlexa
{
    public class Speise
    {
        public string Nr { get; set; }
        public string Name { get; set; }
        public string Bezeichnung { get; set; }
        public double Preis { get; set; }
        public string Kategorie { get; set; }
        public string Optionen { get; set; }


        private static Random randomGenerator = new();

        public static Speise GetRandomFoodFromList(List<Speise> foodRepository)
        {
            return foodRepository[randomGenerator.Next(foodRepository.Count)];
        }
    }

    /// <summary>
    /// map von csv zu POCO
    /// </summary>
    public class SalonikiClassMap : ClassMap<Speise>
    {
        public SalonikiClassMap()
        {
            Map(m => m.Nr).Name("Nr.");
            Map(m => m.Name).Name("Name");
            Map(m => m.Bezeichnung).Name("Bezeichnung");
            Map(m => m.Preis).Name("Preis");
            Map(m => m.Kategorie).Name("Kategorie");
            Map(m => m.Optionen).Name("Optionen");
        }
    }
}
