using Examen18022021Naza.Models;
using Examen18022021Naza.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.ViewComponents
{
    public class ListaGenerosViewComponent: ViewComponent
    {
        private IRepository repo;

        public ListaGenerosViewComponent(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(this.repo.GetGeneros());
        }
    }
}
