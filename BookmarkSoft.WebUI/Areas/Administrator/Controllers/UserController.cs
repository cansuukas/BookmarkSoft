using BookmarkSoft.Core.Service;
using BookmarkSoft.Model.Entities;
using BookmarkSoft.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookmarkSoft.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class UserController : Controller
    {
        private IHostingEnvironment _hostingEnvironment; 
        private readonly ICoreService<User> _us;

        public UserController(ICoreService<User> us, IHostingEnvironment environment)
        {
            _us = us;
            _hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View(_us.GetAll());
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(User user, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                bool imgResult;
                string imgPath = Upload.ImageUpload(files, _hostingEnvironment, out imgResult);

                if (imgResult)
                {
                    user.ImageUrl = imgPath;
                }
                else
                {
                    TempData["Message"] = "Resim yükleme işleminde hata oluştu";
                    return View();
                }


                bool result = _us.Add(user);

                if (result)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Kayıt işlemi esnasında bir hata oluştu";
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu";
            }

            return View(user);
        }

        public IActionResult Update(Guid id)
        {
            return View(_us.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                var item = _us.GetById(user.ID);
                item.FirstName = user.FirstName;
                item.LastName = user.LastName;
                item.Title = user.Title;
                item.EmailAddress = user.EmailAddress;

                bool result = _us.Update(item);
                if (result)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Güncelleme esnasında bir hata oluştu";
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu.Lütfen daha sonra tekrar deneyin";
            }

            return View(user);
        }

        public IActionResult Delete(Guid id)
        {
            _us.Remove(_us.GetById(id));
            return RedirectToAction("Index");
        }

        public IActionResult Activate(Guid id)
        {
            _us.Activate(id);
            return RedirectToAction("Index");
        }
    }
}