using CsvHelper;
using CsvHelper.Configuration;
using SalonikiAlexa.Models;
using System.Globalization;

namespace SalonikiAlexa.Controllers
{
    internal class Reader
    {
        internal static List<T> ReadDishesCsv<T, TMap>(string pathToCsv) where TMap : ClassMap
        {
            if (string.IsNullOrWhiteSpace(pathToCsv) || !File.Exists(pathToCsv) || Path.GetExtension(pathToCsv).ToUpper() != ".CSV")
            {
                return default;
            }

            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("de-DE"))
            {
                HasHeaderRecord = false,
                BadDataFound = null,
                Delimiter = ";",
                Encoding = System.Text.Encoding.UTF8
            };
            using var reader = new StreamReader(pathToCsv);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<TMap>();
            csv.Read();
            var result = csv.GetRecords<T>();
            return result.ToList();
        }
        internal static List<Speise> MapCsvToPoco(string pathToCsv)
        {
            if (string.IsNullOrWhiteSpace(pathToCsv) || !File.Exists(pathToCsv) || Path.GetExtension(pathToCsv).ToUpper() != ".CSV")
            {
                return new List<Speise>();
            }

            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("de-DE"))
            {
                HasHeaderRecord = false,
                BadDataFound = null,
                Delimiter = ";",
                Encoding = System.Text.Encoding.UTF8
            };
            using var reader = new StreamReader(pathToCsv);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<SalonikiClassMap>();
            csv.Read();
            var result = csv.GetRecords<Speise>();
            return result.ToList();
        }
    }
}
