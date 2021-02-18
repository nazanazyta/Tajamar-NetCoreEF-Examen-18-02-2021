using Examen18022021Naza.Extensions;
using Examen18022021Naza.Filters;
using Examen18022021Naza.Models;
using Examen18022021Naza.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Examen18022021Naza.Controllers
{
    public class LibrosController : Controller
    {
        IRepository repo;

        public LibrosController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult LibrosInicio(int? idgenero, int? posicion)
        {
            if (idgenero == null)
            {
                if (posicion == null)
                {
                    posicion = 1;
                }
                int numeropag = 1;
                int numregistros = this.repo.GetNumeroRegistrosLibros();
                String html = "<div>";
                for (int i = 1; i <= numregistros; i += 3)
                {
                    html += "<a href='LibrosInicio?posicion=" + i + "'>Página " + numeropag + "</a> ";
                    numeropag++;
                }
                html += "</div>";
                ViewData["paginas"] = html;
                List<Libro> libros = this.repo.GetGrupoLibros(posicion.Value);
                return View(libros);
            }
            else
            {
                if (posicion == null)
                {
                    posicion = 1;
                }
                int numeropag = 1;
                int numregistros = this.repo.GetNumeroRegistrosLibrosGenero(idgenero.Value);
                String html = "<div>";
                for (int i = 1; i <= numregistros; i += 3)
                {
                    html += "<a href='LibrosInicio?posicion=" + i + "'>Página " + numeropag + "</a> ";
                    numeropag++;
                }
                html += "</div>";
                ViewData["paginas"] = html;
                List<Libro> libros = this.repo.GetGrupoLibros(posicion.Value);
                ViewData["registros"] = numregistros;
                ViewData["genero"] = idgenero;
                return View(libros);
            }
            
        }

        public IActionResult Lista(int? posicion, int idgenero)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeropag = 1;
            int numregistros = this.repo.GetNumeroRegistrosLibrosGenero(idgenero);
            String html = "<div>";
            for (int i = 1; i <= numregistros; i += 3)
            {
                html += "<a href='Lista?posicion=" + i + "?idgenero=" + idgenero + "'>Página " + numeropag + "</a> ";
                numeropag++;
            }
            html += "</div>";
            ViewData["paginas"] = html;
            List<Libro> libros = this.repo.GetGrupoLibrosGenero(posicion.Value, idgenero);
            ViewData["registros"] = numregistros;
            ViewData["genero"] = idgenero;
            return View(libros);
        }

        public IActionResult Details(int idlibro)
        {
            return View(this.repo.GetLibroById(idlibro));
        }

        public IActionResult Pedido(int idlibro)
        {
            List<int> cestalibros;
            //int total;
            if (HttpContext.Session.GetObject<List<int>>("libros") == null)
            {
                cestalibros = new List<int>();
                //total = 0;
            }
            else
            {
                cestalibros = HttpContext.Session.GetObject<List<int>>("libros");
                //total = HttpContext.Session.GetObject<int>("total");
            }
            if (cestalibros.Contains(idlibro) == false)
            {
                cestalibros.Add(idlibro);
                HttpContext.Session.SetObject("libros", cestalibros);
                Libro lib = this.repo.GetLibroById(idlibro);
                //total += lib.Precio;
                //HttpContext.Session.SetObject("total", total);
            }
            return RedirectToAction("Details", this.repo.GetLibroById(idlibro));
        }

        public IActionResult MostrarPedido(int? idlibro)
        {
            List<int> cestalibros = HttpContext.Session.GetObject<List<int>>("libros");
            //int total = HttpContext.Session.GetObject<int>("total");
            if (cestalibros == null)
            {
                return View();
            }
            else
            {
                if (idlibro != null)
                {
                    cestalibros.Remove(idlibro.Value);
                    HttpContext.Session.SetObject("libros", cestalibros);
                    Libro lib = this.repo.GetLibroById(idlibro.Value);
                    //total -= lib.Precio;
                    //HttpContext.Session.SetObject("total", total);
                }
                return View(this.repo.GetCarritoLibros(cestalibros));
            }
        }

        [AuthorizeUser]
        [HttpPost]
        public IActionResult MostrarPedido(List<int> cantidades)
        {
            List<int> cestalibros = HttpContext.Session.GetObject<List<int>>("libros");
            List<Libro> libros = this.repo.GetCarritoLibros(cestalibros);
            TempData.SetObject("libros", libros);
            TempData.SetObject("cantidades", cantidades);
            return RedirectToAction("FinalizarPedido");
        }

        public IActionResult FinalizarPedido()
        {
            List<Libro> libros = TempData.GetObject<List<Libro>>("libros");
            List<int> cantidades = TempData.GetObject<List<int>>("cantidades");
            ViewData["cantidades"] = cantidades;
            TempData.SetObject("libros", libros);
            TempData.SetObject("cantidades", cantidades);
            return View(libros);
        }

        public IActionResult Comprar()
        {
            List<Libro> libros = TempData.GetObject<List<Libro>>("libros");
            List<int> cantidades = TempData.GetObject<List<int>>("cantidades");
            int fac = this.repo.GetMaxIdFactura();
            int i = 0;
            foreach (Libro lib in libros)
            {
                Pedido pedido = new Pedido();
                pedido.IdPedido = this.repo.GetMaxIdPedido();
                pedido.IdFactura = fac;
                pedido.Fecha = DateTime.Now;
                pedido.IdLibro = lib.IdLibro;
                pedido.IdUsuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                pedido.Cantidad = cantidades[i];
                i++;
                this.repo.GuardarPedido(pedido);
            }
            HttpContext.Session.Clear();
            return View();
        }
    }
}
