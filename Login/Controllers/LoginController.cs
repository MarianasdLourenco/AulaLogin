using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Entrar()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Entrar(string usuario, string senha)
        {
            if (usuario == "adm" && senha == "123") //logado
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usuario));
                claims.Add(new Claim(ClaimTypes.Sid, "7"));
                claims.Add(new Claim(ClaimTypes.Role, "AcessarTela"));

                var userIdentity = new ClaimsIdentity(claims, "Acesso");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync("Autenticação", principal, new AuthenticationProperties
                {
                    ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(7)),
                    IsPersistent = true

                }); ;

                return Redirect("/");
            }
            else
            {
                TempData["erro"] = "Usuario e/ou senha inválidos";
                return View();
       
            }
  
        }
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync("Autenticação");
            ViewData["ReturnUrl"] = "/";
            return Redirect("/Login/Entrar");
        }
    }
}



  