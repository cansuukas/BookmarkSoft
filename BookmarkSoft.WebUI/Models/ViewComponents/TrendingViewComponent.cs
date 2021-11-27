using BookmarkSoft.Core.Service;
using BookmarkSoft.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookmarkSoft.WebUI.Models.ViewComponents
{
    public class TrendingViewComponent : ViewComponent
    {
        private readonly ICoreService<Post> _ps;

        public TrendingViewComponent(ICoreService<Post> ps)
        {
            _ps = ps;
        }

        public IViewComponentResult Invoke()
        {
            var posts = _ps.GetActive().OrderByDescending(x => x.ViewCount).Take(5).ToList();
            return View(posts);
        }
    }
}