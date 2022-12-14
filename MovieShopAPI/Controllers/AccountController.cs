using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var userId = await _accountService.RegisterUser(model);
            return Ok(userId);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model);
            if (user != null)
            {
                // create token
                var jwtToken = CreateJwtToken(user);
                return Ok(new {token = jwtToken });
                
                //Clients: iOS, Android, Angular, React, Java
                //create JWT(Json Web Token) token instead of Cookies
                // Client will send email/password to API  POST to Url
                //API will validate the email/password and if valid then API will create the JWT token and send to client
                // Its Client's responsibility to store the token some where
                // Angular, React( local storage or session storage in browser)
                // iOS/Android, device
                // When client need some secure information or needs to perform any operation that requires users to be 
                // authenticated then client need to send the token to the API in the Http Header
                // Once the API receives the token from client it will validate the JWT token and if valid 
                // it will send data back to the client 
                //If JWT token is invalid or token is expired then API will send 401 Unauthorized
                
            }

            throw new UnauthorizedAccessException(" Please check email and password");
            //return Unauthorized(new { errorMessage = "Please check email and password" });

        }

        private string CreateJwtToken(UserLoginSuccessModel user)
        {
            //create the claims 
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country", "USA"),
                new Claim("language", "english"),
                new Claim("isAdmin", (user.Id==1) ? "true": "false")

            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            // specify a secret key so that only API can understand
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));
            
            // Specify the hashing algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
            // Specify the expiration of the token
            var tokenExpiration = DateTime.UtcNow.AddHours(2);
            
            //create an object with all the above information to create the token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients"
            };
            // JwtSecurityTokenHandler() is the class that will that tokenDetails that
            // have information and create the token and return the token back to the caller 
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
