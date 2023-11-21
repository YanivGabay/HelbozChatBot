using HelbozChatBot.Configurations;
using HelbozChatBot.Models;
using Twilio.TwiML;
using Microsoft.Extensions.Options;


namespace HelbozChatBot.Services
{
    public class ChatbotService : IChatbotService
    {
        private readonly string _twilioPhoneNumber;

        public ChatbotService(IOptions<TwilioSettings> twilioSettings)
        {
            _twilioPhoneNumber = twilioSettings.Value.PhoneNumber ?? throw new InvalidOperationException("Twilio phone number is not configured properly.");
        }


        public string ProcessIncomingMessage(string from, string body)
        {
            var response = new MessagingResponse();
            body = body.Trim(); // Trimming any whitespace

            // Check if the message contains certain keywords
            if (body.Contains("שעות") || body.Contains('1'))
            {
                response.Message("יום א-ה - 09:00-20:00, ימי שישי 09:00-13:30", from, _twilioPhoneNumber);
            }
            else if (body.Contains("חלבון") || body.Contains('2'))
            {
                response.Message("בטח! מוזמנים להכנס לאתר: www.helboz.co.il", from, _twilioPhoneNumber);
            }
            // Add more conditions as per your chatbot logic
            else
            {
                response.Message("היי ברוך הבא לחלבוז:", from, _twilioPhoneNumber);
            }

            Console.WriteLine($"Received response: {response}");
            return response.ToString();
        }

    }


}
