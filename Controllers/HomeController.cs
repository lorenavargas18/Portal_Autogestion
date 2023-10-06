using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDM.Models;

namespace TDM.Controllers;

public class HomeController : Controller
{
    private readonly MyDbContext _context;
    private readonly ILogger<HomeController> _logger;

     public HomeController(MyDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
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
