using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace SoccerManagerTests.Controllers
{
    static class ControllerBaseExtensions
    {
        public static void SetUserEmail(this ControllerBase controller, string email)
        {
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, email)
                    }
                )
            );
            var httpContext = new DefaultHttpContext();
            httpContext.User = user;
            controller.ControllerContext.HttpContext = httpContext;
        }
    }
}
