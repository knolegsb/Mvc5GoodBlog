using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Mvc5GoodBlog.ViewModels;

namespace Mvc5GoodBlog.Controllers
{
    public class AuthorController : Controller
    {
        public int ItemsByPage = 5;

        // GET: Author
        [OutputCache(Duration = 3600, VaryByParam ="*", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(int id, int page = 1)
        {
            IList<Post> posts = new List<Post>();
            using(GoodBlogDbContext db = new GoodBlogDbContext())
            {
                int startIndex = page <= 1 ? 0 : (page - 1) * this.ItemsByPage;

                posts = db.Posts.Where(p => p.UserId == id)
                                .Include(p => p.Category)
                                .Include(p => p.User)
                                .OrderByDescending(p => p.Created)
                                .Skip(startIndex)
                                .Take(this.ItemsByPage)
                                .ToList();
            }

            ViewBag.id = id;

            if (HttpContext.Cache["Post"] == null)
                HttpContext.Cache["Post"] = DateTime.UtcNow.ToString();
            Response.AddCacheItemDependency("Post");
            return View(posts);
        }

        [ChildActionOnly]
        public ActionResult Pager(int id)
        {
            int count = 0;
            int pages = 1;
            using(GoodBlogDbContext db = new GoodBlogDbContext())
            {
                count = db.Posts.Where(p => p.UserId == id).Count();
            }
            pages = (count / this.ItemsByPage) + 1;

            return PartialView("~/Views/Shared/_Pager.cshtml", new PagerModel() { Count = count, Pages = pages });
        }
    }
}