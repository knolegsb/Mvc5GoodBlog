using Mvc5GoodBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5GoodBlog.Controllers
{
    public class CategoryController : Controller
    {
        public int ItemsByPage = 5;

        // GET: Category
        [OutputCache(Duration = 3600, VaryByParam = "*", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(string id, int page = 1)
        {
            IList<Post> posts = new List<Post>();
            using(GoodBlogDbContext db = new GoodBlogDbContext())
            {
                int startIndex = page <= 1 ? 0 : (page - 1) * this.ItemsByPage;

                posts = db.Posts.Where(p => p.Category.Slug == id)
                                .Include(p => p.Category)
                                .Include(p => p.User)
                                .OrderByDescending(p => p.Created)
                                .Skip(startIndex)
                                .Take(this.ItemsByPage)
                                .ToList();
            }

            ViewBag.Slug = id;

            if (HttpContext.Cache["Post"] == null)
                HttpContext.Cache["Post"] = DateTime.UtcNow.ToString();
            Response.AddCacheItemDependency("Post");

            return View(posts);
        }

        [ChildActionOnly]
        public ActionResult Pager(string id)
        {
            int count = 0;
            int pages = 1;
            using(GoodBlogDbContext db = new GoodBlogDbContext())
            {
                count = db.Posts.Where(p => p.Category.Slug == id).Count();
            }

            pages = (count / this.ItemsByPage) + 1;

            return PartialView("~/Views/Shared/_Pager.cshtml", new PagerModel() { Count = count, Pages = pages });
        }
    }
}