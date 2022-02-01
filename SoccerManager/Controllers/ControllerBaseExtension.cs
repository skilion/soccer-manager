using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SoccerManager.Controllers
{
    static class ControllerBaseExtensions
    {
        public static string GetUserEmail(this ControllerBase controller)
        {
            return controller.User.FindFirstValue(ClaimTypes.Email);
        }
    }
}
