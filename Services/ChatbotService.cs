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


            var israelTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
            var currentIsraelTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, israelTimeZone);
            bool isStoreOpen = IsStoreOpen(currentIsraelTime);

            if (!isStoreOpen)
                OffHoursFlow(from, body, response);
            else
                OpenHoursFlow(from, body, response);

            return response.ToString();
        }
        public bool IsStoreOpen(DateTimeOffset currTime)
        {
            var openingTime = new TimeSpan(9, 0, 0); // 9:00 AM
            var closingTime = new TimeSpan(20, 0, 0); // 20:00 PM

            // Check if the current time is within the store hours
            return currTime.TimeOfDay >= openingTime && currTime.TimeOfDay <= closingTime;
        }
         public string OpenHoursFlow(string from, string body, MessagingResponse response)
        {
            return response.Message("ברוכים הבאים לחלבון זמין! נציג אנושי תכף אתכם\n" +
                "יכולים בינתיים להכנס לאתר שלנו: www.helboz.co.il" +
                "שעות הפתיחה הם" +
                "יום א-ה - 09:00-20:00, ימי שישי 09:00-13:30\" ")
        }
        public string OffHoursFlow(string from, string body, MessagingResponse response)
        {
            // Check if the message contains certain keywords
            if (body.Contains("שעות") || body.Contains('1'))
            {
                response.Message("יום א-ה - 09:00-20:00, ימי שישי 09:00-13:30", from, _twilioPhoneNumber);
            }
            else if (body.Contains("מגיעים") && body.Contains("איך") || body.Contains('2'))
            {

                string wazeLink = "https://bit.ly/HelbozLocation";
                response.Message("צאלון 21 מודיעין, מודיעין סנטר" +
                      "\n" +
                     "אנחנו נמצאים בקומה התחתונה,בחלק השמאלי של המרכז" +
                     "\n" +
                     " ליד פיצה רומי ומספרת אקספרס" +
                       "\n" +
                     " Waze Link:" +
                     wazeLink, from, _twilioPhoneNumber);
            }
            // Add more conditions as per your chatbot logic
            else
            {
                response.Message(" \n היי ברוכים לחלבוז - חלבון זמין,כאן צ'אט בוט חלבוני לשירותכם" +
                    "\n רשמו בהודעה רק את המספר הרצוי,ותופנו בהתאם:" +
                    "\n 1 - שעות פתיחה" +
                    "\n 2 - כתובת והוראות הגעה" +
                    "\n 3 - נציג אנושי תותח" +
                    "\n מוזמנים להכנס לאתר שלנו :" +
                    "www.helboz.co.il", from, _twilioPhoneNumber);
            }

            Console.WriteLine($"Received response: {response}");
        }
    }


}
