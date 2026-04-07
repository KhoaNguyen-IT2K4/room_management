using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_room_management_C_.Models;
using BCrypt.Net;

namespace project_room_management_C_.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly RoomManagementContext _context;

        public AuthenticateController(RoomManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ShowLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString().ToLower())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );

                TempData["Success"] = $"Xin chào <b>{user.Name}</b>";
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["Error"] = "Sai tài khoản hoặc mật khẩu";
            return RedirectToAction("ShowLogin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("ShowLogin", "Authenticate");
        }
    }
}