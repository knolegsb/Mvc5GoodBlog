using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Mvc5GoodBlog.ViewModels;

namespace Mvc5GoodBlog.Controllers
{
    public class SideBarController : Controller
    {
        // GET: SideBar
        [ChildActionOnly]
        public ActionResult Index()
        {
            HttpContext.Cache.Insert("Post", 1);
            Response.AddCacheItemDependency("Post");

            IList<Category> categories = new List<Category>();
            using(GoodBlogDbContext db = new GoodBlogDbContext())
            {
                categories = db.Categories
                    .Include(c => c.Posts)
                    .ToList();
            }

            IList<Post> posts = new List<Post>();
            using (GoodBlogDbContext db = new GoodBlogDbContext())
            {
                posts = db.Posts
                    .OrderByDescending(p => p.Created)
                    .Take(2)
                    .ToList();
            }
            
            return PartialView("_SideBar", new SideBarModel() { Categories = categories, Posts = posts });
        }
    }
}