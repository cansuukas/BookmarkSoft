using BookmarkSoft.Core.Service;
using BookmarkSoft.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookmarkSoft.WebUI.Controllers
{

  

    public class HomeController : Controller
    {
        private readonly ICoreService<Category> _cs;
        private readonly ICoreService<Post> _ps;
        private readonly ICoreService<User> _us;

        public HomeController(ICoreService<Category> cs, ICoreService<Post> ps, ICoreService<User> us)
        {
            _cs = cs;
            _ps = ps;
            _us = us;
        }

        public IActionResult Index()
        {
            return View(_ps.GetActive());
        }

       
        public IActionResult Post(Guid id)
        {
         

            var postModel = _ps.GetById(id); 
            postModel.ViewCount++; 
            _ps.Update(postModel);
            return View(Tuple.Create<Post, User>(postModel, _us.GetById(postModel.UserID)));
        }

        public IActionResult Posts(Guid id)
        {
            return View(_ps.GetDefault(x => x.CategoryID == id).ToList());
        }
    }
}