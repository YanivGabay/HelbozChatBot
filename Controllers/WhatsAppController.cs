using Microsoft.AspNetCore.Mvc;
using Twilio.TwiML;
using HelbozChatBot.Services;
using HelbozChatBot.Models;

[ApiController]
[Route("[controller]")]
public class WhatsAppController : ControllerBase
{
    private readonly IChatbotService _chatbotService;

    public WhatsAppController(IChatbotService chatbotService)
    {
        _chatbotService = chatbotService;
    }

    [HttpPost("ReceiveMessage")]
    public IActionResult ReceiveMessage([FromForm] string From, [FromForm] string Body)
    {
        var response = _chatbotService.ProcessIncomingMessage(From, Body);
        Console.WriteLine($"Received message: {Body}");
        return Content(response, "text/xml");
    }
}
