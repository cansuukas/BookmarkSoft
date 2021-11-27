using BookmarkSoft.Core.Service;
using BookmarkSoft.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookmarkSoft.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize] 
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _cs;

        public CategoryController(ICoreService<Category> cs)
        {
            _cs = cs;
        }

        public IActionResult Index()
        {
            return View(_cs.GetAll());
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Category category)
        {
            if (ModelState.IsValid)
            {
                bool result = _cs.Add(category);
                if (result)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Kayıt işlemi esnasında hata oluştu";
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu.Lütfen daha sonra tekrar deneyin";
            }

            return View(category);
        }

        public IActionResult Update(Guid id)
        {
            return View(_cs.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                Category item = _cs.GetById(category.ID);
                item.CategoryName = category.CategoryName;
                item.Description = category.Description;

                bool result = _cs.Update(item);
                if (result)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Güncelleme esnasında bir hata oluştu";
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu.Lütfen daha sonra tekrar deneyin";
            }

            return View(category);
        }

        public IActionResult Delete(Guid id)
        {
            _cs.Remove(_cs.GetById(id));
            return RedirectToAction("Index");
        }

        public IActionResult Activate(Guid id)
        {
            _cs.Activate(id);
            return RedirectToAction("Index");
        }
    }
}