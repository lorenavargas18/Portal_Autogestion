using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using TDM.Models;
using System.Text.Json;

namespace TDM.Controllers;

public class HomeController : Controller
{
    private readonly MyDbContext _context;
    private readonly HttpClient _httpClient;

    private readonly ILogger<HomeController> _logger;

    public HomeController(MyDbContext context, IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://prod-01.brazilsouth.logic.azure.com:443/");
            _logger = logger;
             TwilioClient.Init("ACbb5c57eb8b5f8fadf92ef462b263c525", "a3ede6e781cf837b2459880971536a66");
        }

    public async Task<IActionResult> Index()
    {
     
        
        return View();
    }

    private async Task TestDatabaseConnection()
    {
        try
        {
            await _context.Database.OpenConnectionAsync();
            Console.WriteLine("Conexión a la base de datos exitosa.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
        }
        finally
        {
            _context.Database.CloseConnection();
        }
    }
     
       public async Task<IActionResult> ConsultarApi()
{
    // Crea y envía el mensaje
    var message = await MessageResource.CreateAsync(
        body: "5432",
        from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
        to: new Twilio.Types.PhoneNumber($"whatsapp:+573016211081")
    );

    Console.WriteLine($"Mensaje enviado con ID: {message.Sid}");

    return Ok($"Mensaje enviado con ID: {message.Sid}");
}


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



}
