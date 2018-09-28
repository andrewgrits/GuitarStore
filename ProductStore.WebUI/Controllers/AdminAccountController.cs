using ProductStore.Domain.Abstract;
using ProductStore.Domain.Concrete;
using ProductStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductStore.WebUI.Controllers
{
    public class AdminAccountController : Controller
    {
        private IAdminAccountRepository repository;

        public AdminAccountController(IAdminAccountRepository repo)
        {
            repository = repo;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminAccountViewModel account, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = repository.ValidateUser(account.Login, account.Password);

                if (result)
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                    return View();
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            var httpCoockie = HttpContext.Response.Cookies["autCoockie"];
            if(httpCoockie!=null)
            {
                httpCoockie.Value = string.Empty;
            }

            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult ChangeAccountData()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeAccountData(ChangeAccountDataViewModel adminAccount)
        {
            if (ModelState.IsValid)
            {
                if (repository.SaveAdminAccount(adminAccount.Login, adminAccount.Password,
                    System.Web.HttpContext.Current.User.Identity.Name, adminAccount.OldPassword))
                {
                    TempData["message"] = string.Format("Изменения логина/пароля успешно сохранены");
                    return RedirectToAction("Index", "Admin");
                }
                TempData["errorMessage"] = string.Format("Ошибка! Введённый логин или старый пароль неверны!");
                return View();
            }
            return View();
        }
    }
}