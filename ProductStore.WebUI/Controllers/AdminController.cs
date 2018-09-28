using System.Web.Mvc;
using ProductStore.Domain.Abstract;
using ProductStore.Domain.Entities;
using System.Linq;
using System.Web;

namespace ProductStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IGuitarRepository repository;

        public AdminController(IGuitarRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Guitars);
        }

        public ViewResult Edit(int Id)
        {
            Guitar guitar = repository.Guitars
                .FirstOrDefault(p => p.Id == Id);
            return View(guitar);
        }

        [HttpPost]
        public ActionResult Edit(Guitar guitar, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    guitar.ImageMimeType = image.ContentType;
                    guitar.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(guitar.ImageData, 0, image.ContentLength);
                    repository.SaveProduct(guitar, true);
                }
                else
                {
                    repository.SaveProduct(guitar, false);
                }
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", guitar.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(guitar);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Guitar());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Guitar deletedProduct = repository.DeleteProduct(id);

            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удалён",
                    deletedProduct.Name);
            }

            return RedirectToAction("Index");
        }
    }
}