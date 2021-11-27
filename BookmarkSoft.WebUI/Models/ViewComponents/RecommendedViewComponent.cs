using BookmarkSoft.Core.Service;
using BookmarkSoft.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookmarkSoft.WebUI.Models.ViewComponents
{
    public class RecommendedViewComponent : ViewComponent
    {
        private readonly ICoreService<Post> _ps;

        public RecommendedViewComponent(ICoreService<Post> ps)
        {
            _ps = ps;
        }

        public IViewComponentResult Invoke()
        {
            var recommended = _ps.GetActive().OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            return View(recommended);
        }
    }
}