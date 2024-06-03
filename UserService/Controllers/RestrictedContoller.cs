using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Model;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestrictedContoller : ControllerBase
    {
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Buenos Dias, {currentUser}! You are admin");
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult UserEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Buenos Dias, {currentUser}! You are user");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                var roleValue = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                RoleId roleId;
                if (Enum.TryParse(roleValue, out roleId))
                {
                    var role = new Role
                    {
                        RoleId = roleId,
                        Name = roleValue
                    };

                    return new User
                    {
                        Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                        Role = role
                    };
                }
            }
            return null;
        }
    }
}