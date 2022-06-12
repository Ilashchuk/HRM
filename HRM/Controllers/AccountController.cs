using HRM.Data;
using HRM.Models;
using HRM.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRM.Controllers
{
    public class AccountController : Controller
    {
        private HRMContext db;
        public AccountController(HRMContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login and(or) password");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Company? company = await db.Companies.FirstOrDefaultAsync(c => c.Name == model.Company);
                if (company == null)
                {
                    db.Companies.Add(new Company { Name = model.Company });
                    await db.SaveChangesAsync();

                    User? user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user == null)
                    {
                        //adding user(role: HR) to db
                        user = new User { FullName = model.FullName, 
                            Email = model.Email, 
                            Password = model.Password,
                            StartDate = DateTime.Now, 
                            CompanyId = db.Companies.First(c => c.Name == model.Company).Id,
                            RoleTypeId = db.RoleTypes.First(r => r.Name == "HR").Id,
                            UserStatusId = db.Statuses.First(s => s.Name == "Working").Id
                        };
                        user.Company = db.Companies.First(c => c.Id == user.CompanyId);
                        user.Status = db.Statuses.First(s => s.Id == user.UserStatusId);

                        RoleType? role = await db.RoleTypes.FirstOrDefaultAsync(r => r.Id == user.RoleTypeId);
                        if (role == null)
                        {
                            user.RoleType = role;
                        }
                            
                        db.Add(user);
                        await db.SaveChangesAsync();

                        await Authenticate(user);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Incorrect login and(or) password");
                }
                else
                    ModelState.AddModelError("", "Company is olready registered");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, db.RoleTypes.First(r => r.Id == user.RoleTypeId).Name)
            };
            //claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email));
            //claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, db.RoleTypes.First(r => r.Id == user.RoleTypeId).Name));
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
