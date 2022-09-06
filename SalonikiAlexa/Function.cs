using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using SalonikiAlexa.Controllers;
using SalonikiAlexa.Models;
using SalonikiAlexa.Resources;

//Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SalonikiAlexa
{
    public class Function
    {
        public string SALONIKI_CSV_NAME = "SalonikiSpeisekarte.csv";

        public List<Speise> Speisen { get; set; }
        private Speise Vorspeise;
        private Speise Hauptgericht;
        private Speise Getraenk;
        private MessageRepository Messages = new();
        //Function-Handler für Anfragen an Alexa
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            //Logging
            var log = context.Logger;

            //Food-Repository füllen, falls wir hier nix drin haben sollten (z.B. beim ersten Request)
            if (Speisen == null || !Speisen.Any())
            {
                log.LogInformation($"Pfad {Path.Combine(Environment.CurrentDirectory, "Resources", SALONIKI_CSV_NAME)}");
                Speisen = Reader.ReadDishesCsv<Speise, SalonikiClassMap>(Path.Combine(Environment.CurrentDirectory, "Resources", SALONIKI_CSV_NAME));
            }

            if (input.Request is LaunchRequest)
            {
                return Response.MakeSkillResponse(Messages.WelcomeMessage, false, Messages.HelpReprompt);
            }
            var intentRequest = input.Request as IntentRequest;
            switch (intentRequest?.Intent.Name)
            {
                case "AMAZON.CancelIntent":
                    return ResponseBuilder.Tell(Messages.CancelMessage);
                case "AMAZON.HelpIntent":
                    return Response.MakeSkillResponse(Messages.HelpMessage, false, Messages.HelpReprompt);
                case "AMAZON.StopIntent":
                case "AMAZON.FallbackIntent":
                    return ResponseBuilder.Tell(Messages.StopMessage);
                case "Vorspeise":
                    Vorspeise = GenerateVorspeise(Vorspeise);
                    return TellCurrentlySelectedMenu();
                case "Hauptgericht":
                    Hauptgericht = GenerateHauptgericht(Hauptgericht);
                    return TellCurrentlySelectedMenu();
                case "Gertraenk":
                    Getraenk = GenerateGetraenk(Getraenk);
                    return TellCurrentlySelectedMenu();
                default:
                    GenerateNewMenu(out Vorspeise, out Hauptgericht, out Getraenk);
                    return TellCurrentlySelectedMenu();
            }
        }

        private SkillResponse TellCurrentlySelectedMenu()
        {
            var gesamtBetrag = Math.Round(Vorspeise.Preis + Hauptgericht.Preis + Getraenk.Preis, 2);
            var resultMessage = $"{Messages.ResultMessage} Zuerst gibt es {Vorspeise.Name} {Vorspeise.Bezeichnung}, dann {Hauptgericht.Name} {Hauptgericht.Bezeichnung}." +
                $" Zu trinken gibt es {Getraenk.Name} {Getraenk.Bezeichnung}. Gesamtkosten {gesamtBetrag} Euro.";
            var cardContent = $"Vorspeise: {Vorspeise.Name} {Vorspeise.Bezeichnung}\nHauptgericht: {Hauptgericht.Name} {Hauptgericht.Bezeichnung}\n Getränk: {Getraenk.Name}\nPreis: {gesamtBetrag}";
            return Response.MakeSkillResponse(resultMessage, false, Messages.HelpReprompt, "Essen im Saloniki", cardContent);
        }

        private void GenerateNewMenu(out Speise vorspeise, out Speise hauptgericht, out Speise getraenk)
        {
            vorspeise = GenerateVorspeise();
            hauptgericht = GenerateHauptgericht();
            getraenk = GenerateGetraenk();
        }

        private Speise GenerateGetraenk(Speise getraenk = null)
        {
            var getraenke = Speisen.Where(x => x.Kategorie == "Getränke" && x.Name != getraenk?.Name).ToList();
            getraenk = Speise.GetRandomFoodFromList(getraenke);
            return getraenk;
        }

        private Speise GenerateHauptgericht(Speise hauptgericht = null)
        {
            var hauptgerichte = Speisen.Where(x => x is { Kategorie: not "Kalte und warme Vorspeisen", Kategorie: not "Getränke", Kategorie: not "Saucen", Kategorie: not "Beilagen" }
            && x.Name != hauptgericht?.Name).ToList();
            hauptgericht = Speise.GetRandomFoodFromList(hauptgerichte);
            return hauptgericht;
        }

        private Speise GenerateVorspeise(Speise vorspeise = null)
        {
            var vorspeisen = Speisen.Where(x => x.Kategorie == "Kalte und warme Vorspeisen" && x.Name != vorspeise?.Name).ToList();
            vorspeise = Speise.GetRandomFoodFromList(vorspeisen);
            return vorspeise;
        }
    }
}