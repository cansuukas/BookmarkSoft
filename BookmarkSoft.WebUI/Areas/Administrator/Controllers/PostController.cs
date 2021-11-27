using BookmarkSoft.WebUI.Models;
using BookmarkSoft.Core.Service;
using BookmarkSoft.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookmarkSoft.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class PostController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ICoreService<Post> _ps;
        private readonly ICoreService<Category> _cs;

        public PostController(ICoreService<Post> ps, ICoreService<Category> cs, IHostingEnvironment environment)
        {
            _cs = cs;
            _ps = ps;
            _hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View(_ps.GetAll());
        }

        public IActionResult Insert()
        {
            ViewBag.Categories = new SelectList(_cs.GetActive(), "ID", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Post post, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                post.UserID = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "ID").Value);

                bool imgResult;
                string imgPath = Upload.ImageUpload(files, _hostingEnvironment, out imgResult);

                if (imgResult)
                {
                    post.ImagePath = imgPath;
                }
                else
                {
                    TempData["Message"] = "Resim yükleme işleminde hata oluştu";
                    return View();
                }


                bool result = _ps.Add(post);

                if (result)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Kayıt işlemi esnasında bir hata oluştu";
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu";
            }

            return View(post);
        }

        public IActionResult Update(Guid id)
        {
            ViewBag.Categories = new SelectList(_cs.GetActive(), "ID", "CategoryName");
            return View(_ps.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(Post post)
        {
            if (ModelState.IsValid)
            {
                var item = _ps.GetById(post.ID);
                item.Title = post.Title;
                item.Tags = post.Tags;
                item.PostDetail = post.PostDetail;

                bool result = _ps.Update(item);
                if (result)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Güncelleme esnasında bir hata oluştu";
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu.Lütfen daha sonra tekrar deneyin";
            }

            return View(post);
        }

        public IActionResult Delete(Guid id)
        {
            _ps.Remove(_ps.GetById(id));
            return RedirectToAction("Index");
        }

        public IActionResult Activate(Guid id)
        {
            _ps.Activate(id);
            return RedirectToAction("Index");
        }
    }
}