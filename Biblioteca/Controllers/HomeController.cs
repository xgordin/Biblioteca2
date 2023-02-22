using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;



namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.CheckUser = HttpContext.Session.GetString("user");

            return View();

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            BibliotecaContext bibliotecaContext = new BibliotecaContext();
            var validate = bibliotecaContext.Usuario
                             .Where(x => x.loginUsuario == login &&
                                         x.senhaUsuario == UsuarioService.GenerateHashPassword(senha))
                             .FirstOrDefault();

            if (validate is null)
            {
                ViewData["Erro"] = "Senha inválida";
                return View();
            }
            else
            {
                ViewData["User"] = login;
                HttpContext.Session.SetString("user", login);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
