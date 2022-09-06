namespace SalonikiAlexa.Resources
{
    internal class MessageRepository
    {
        // Name des Alexa-Dienstes
        public const string INVOCATION_NAME = "Saloniki Apen";
        public string HelpMessage { get; set; }
        public string WelcomeMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string HelpReprompt { get; set; }
        public string StopMessage { get; set; }
        public string ResultMessage { get; set; }
        public string CancelMessage { get; set; }
        public string TitleCard { get; set; }

        public MessageRepository()
        {
            WelcomeMessage = $"Willkommen zum Skill {INVOCATION_NAME}. Um ein neues Menü aus Vorspeise, Hauptgericht und Getränk generieren zu lassen sag, {INVOCATION_NAME} neues Menü erstellen";
            ErrorMessage = "Entschuldige, ich konnte deine Anfrage leider nicht verstehen. Bitte versuch es erneut";
            HelpMessage = $"Um ein neues Menü aus Vorspeise, Hauptgericht und Getränk generieren zu lassen sag, {INVOCATION_NAME} neues Menü erstellen";
            HelpReprompt = $"Wenn eine andere Vorspeise willst sag Alexa, gib mir eine neue Vorspeise. Wenn du ein anderes Hauptgericht gewürfelt bekommen willst sagt Alexa gib mir ein neues Hauptgericht" +
                $" und wenn du ein neues Getränk möchtest sag einfach Alexa, gib mir ein anderes Getränk. Um ein neues Menü aus Vorspeise, Hauptgericht und Getränk generieren zu lassen sag, {INVOCATION_NAME} neues Menü erstellen";
            StopMessage = "Bis zum nächsten mal!";
            CancelMessage = "Vorgang abgebrochen";
            ResultMessage = "Hier ist dein gewünschtes Essen!";
            TitleCard = $"Willkommen bei {INVOCATION_NAME}";
        }
    }
}
