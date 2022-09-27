using System.Net;
using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginModel model)
    {
        var userSuccess = await _accountService.ValidateUser(model);
        if (userSuccess!= null)
        {
            // password matches
            // redirect to home page
            // create a cookie, cookies are always sent from browser automatically to server
            // Inside the cookie we store encrypted information (User claims) that Server can recognize and tell 
            // whether user is logged in or not
            // Cookie should have an expiration time
            // 2 hours
            // create claims userid, email, firstname, lastname
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userSuccess.Email),
                new Claim(ClaimTypes.NameIdentifier, userSuccess.Id.ToString()),
                new Claim(ClaimTypes.Surname, userSuccess.LastName),
                new Claim(ClaimTypes.GivenName, userSuccess.FirstName),
                new Claim("language", "english"),

            };
            // create cookie and cookie will have the above claims information
            // along with expiration time, don't store above information in the cookie as plain text, instead encrypt the above information
            
            // We need to tell ASP.NET application that we are using Cookie based Authentication so that
            // ASp.NET can generate Cookie based on the settings we provide
            
            //identity object that will identify the user with claims
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            // principal object which is used by ASP.NET to recognize the USER
            // create cookie with above information
            
            //HttpContext => Encapsulates your Http Request information
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            
            return LocalRedirect("~/");
        }
        return View(model);
    }
    public IActionResult Register()
    {
        // showing empty register view
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterModel model)
    {
        if (ModelState.IsValid)
        {
            //Model Binding
            var userId = await _accountService.RegisterUser(model);
            // redirect to login page
            return RedirectToAction("Login");
        }

        return View(model);

    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }
}