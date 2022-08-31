using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class CastController : Controller
{
    private readonly ICastService _castService;

    public CastController(ICastService castService)
    {
        _castService = castService;
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        // Go to CastService => CastRepository and get cast details from Cast Table
        //Go to database and get the cast information by
        //cast id and send the data(Model) to the view
        // ADO.NET
        // Dapper Stack Overflow -> Micro ORM
        // Entity Framework Core -> Full ORM
        var castDetails = await _castService.GetCastDetails(id);
        return View(castDetails);
    }
}