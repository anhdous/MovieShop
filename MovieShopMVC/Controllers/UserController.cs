using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopMVC.Controllers;
[Authorize]
public class UserController : Controller
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;

    public UserController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    [HttpGet]
    public async Task<IActionResult> Purchases()
    {
        // Get all the movies purchased by user by user id
        // httpcontext.user.claims and then call the database and get the information to the view
        // before doing this, have to check the IsAuthenticated flag
        var userId = _currentUser.UserId;
        var purchases = await _userService.GetAllPurchasesForUser(userId);
        return View(purchases);
    }
    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> EditProfile(UserEditModel model)
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> BuyMovie()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> FavoriteMovie()
    {
        return View();
    }
    
}