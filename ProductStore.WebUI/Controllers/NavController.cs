using ProductStore.Domain.Abstract;
using ProductStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGuitarRepository repository;

        public NavController(IGuitarRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            var categories = new NavViewModel
            {
                Categories = repository.Guitars
                 .Select(product => product.Category)
                 .Distinct()
                 .OrderBy(x => x),
                CurrentCategory = category
            };
            return PartialView(categories);
        }
    }
}