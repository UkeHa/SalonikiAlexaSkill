using Alexa.NET.Response;

namespace SalonikiAlexa.Controllers
{
    internal class Response
    {
        /// <summary>
        /// Antwortet dem Benutzer via Alexa
        /// </summary>
        /// <param name="outputSpeech"></param>
        /// <param name="shouldEndSession"></param>
        /// <param name="repromptText"></param>
        /// <returns></returns>
        internal static SkillResponse MakeSkillResponse(string outputSpeech,
            bool shouldEndSession,
            string repromptText, string title = "", string cardContent = "")
        {
            StandardCard card = null;

            card = new StandardCard()
            {
                Title = title,
                Content = cardContent
            };

            var response = new ResponseBody
            {
                ShouldEndSession = shouldEndSession,
                OutputSpeech = new PlainTextOutputSpeech { Text = outputSpeech },
                Card = card
            };

            if (repromptText != null)
            {
                response.Reprompt = new Reprompt() { OutputSpeech = new PlainTextOutputSpeech() { Text = repromptText } };
            }

            var skillResponse = new SkillResponse
            {
                Response = response,
                Version = "1.0"
            };
            return skillResponse;
        }
    }
}
