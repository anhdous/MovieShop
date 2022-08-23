using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class MoviesController : Controller
{
    public IActionResult Details(int id)
    {
        //Go to database and get the movie information by
        //movie id and send the data(Model) to the view
        // ADO.NET
        // Dapper Stack Overflow -> Micro ORM
        // Entity Framework Core -> Full ORM
        return View();
    }
}