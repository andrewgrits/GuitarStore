using ProductStore.Domain.Abstract;
using ProductStore.Domain.Entities;
using ProductStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductStore.WebUI.Controllers
{
    public class GuitarController : Controller
    {
        private IGuitarRepository repository;
        public int pageSize = 4;

        public GuitarController(IGuitarRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            var model = new ProductListViewModel
            {
                Guitars = repository.Guitars
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Guitars.Count() :
                    repository.Guitars.Where(p => p.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int id)
        {
            Guitar guitar = repository.Guitars
                .FirstOrDefault(g => g.Id == id);

            if (guitar != null)
            {
                return File(guitar.ImageData, guitar.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}