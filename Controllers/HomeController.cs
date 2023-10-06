using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using TDM.Models;

namespace TDM.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly MyDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(MyDbContext context, ILogger<HomeController> logger, IHttpClientFactory clientFactory)
    {
        _context = context;
        _logger = logger;
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
        await TestDatabaseConnection();
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

     [HttpPost]
    public async Task<IActionResult> EnviarCodigo(string cedula)
    {
        var client = _clientFactory.CreateClient();
        var apiUrl = $"https://api.ejemplo.com/enviarCodigo/{cedula}";

        try
        {
            // Envia la solicitud para enviar el código de verificación al número de teléfono asociado con la cédula
            await client.PostAsync(apiUrl, null);

            // Muestra el modal para que el usuario ingrese el código de verificación
            return PartialView("_Modal");
        }
        catch (HttpRequestException)
        {
            // Maneja errores de solicitud HTTP
            return BadRequest("Error al enviar el código de verificación.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> ValidarCodigo(string codigo)
    {
        var client = _clientFactory.CreateClient();
        var apiUrl = $"https://api.ejemplo.com/validarCodigo/{codigo}";

        try
        {
            // Valida el código ingresado por el usuario
            var respuesta = await client.PostAsync(apiUrl, null);

            if (respuesta.IsSuccessStatusCode)
            {
                // Si el código es válido, redirige al usuario a la aplicación
                return RedirectToAction("App");
            }
            else
            {
                // Si el código no es válido, muestra un mensaje de error en el modal
                return PartialView("_Modal", "Código incorrecto. Por favor, inténtelo de nuevo.");
            }
        }
        catch (HttpRequestException)
        {
            // Maneja errores de solicitud HTTP
            return PartialView("_Modal", "Error al validar el código. Por favor, inténtelo de nuevo.");
        }
    }

    public IActionResult App()
    {
        // Lógica para mostrar la aplicación al usuario después de la validación exitosa
        return View();
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
