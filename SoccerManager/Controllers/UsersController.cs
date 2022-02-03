using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoccerManager.Helpers;
using SoccerManager.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoccerManager.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesErrorResponseType(typeof(void))]
    public class UsersController : ControllerBase
    {
        private readonly SoccerManagerDbContext context;
        private readonly ITeamGenerator teamGenerator;
        private readonly SymmetricSecurityKey jwtIssuerKey;

        public UsersController(SoccerManagerDbContext context, ITeamGenerator teamGenerator, IConfiguration configuration)
        {
            this.context = context;
            this.teamGenerator = teamGenerator;
            string issuerSigningKey = configuration["JwtSettings:IssuerSigningKey"];
            jwtIssuerKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
        }

        /// <summary>
        /// Authenticates an user
        /// </summary>
        /// <returns>A JWT bearer token</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate([FromBody] AuthRequest request)
        {
            if (!AuthenticateUser(request))
            {
                return Unauthorized();
            }

            var token = BuildJwtToken(request.Email);
            var response = new AuthResponse
            {
                Bearer = token
            };

            return Ok(response);   
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <remarks>
        /// The user will have a random team generated
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            if (context.Users.Where(user => user.Email == registerRequest.Email).Any())
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            User user = new()
            {
                Email = registerRequest.Email,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(registerRequest.Password),
                Team = teamGenerator.Generate()
            };

            context.Users.Add(user);
            context.SaveChanges();

            return Ok();
        }

        private bool AuthenticateUser(AuthRequest request)
        {
            var user = context.Users.Where(user => user.Email == request.Email);
            if (user.Count() != 1)
            {
                return false;
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Single().PasswordHash))
            {
                return false;
            }
            return true;
        }

        private string BuildJwtToken(string email)
        {
            var signinCredentials = new SigningCredentials(jwtIssuerKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, email),
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
