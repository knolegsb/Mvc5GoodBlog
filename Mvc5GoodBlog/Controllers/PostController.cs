using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Mvc5GoodBlog.ViewModels;

namespace Mvc5GoodBlog.Controllers
{
    public class PostController : Controller
    {
        public int ItemsByPage = 5;
        // GET: Post
        [OutputCache(Duration = 3600, VaryByParam = "*", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(int page = 1)
        {
            IList<Post> posts = new List<Post>();
            using(GoodBlogDbContext db = new GoodBlogDbContext())
            {
                int startIndex = page <= 1 ? 0 : (page - 1) * this.ItemsByPage;

                posts = db.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .OrderByDescending(p => p.Created)
                    .Skip(startIndex)
                    .Take(this.ItemsByPage)
                    .ToList();
            }

            if (HttpContext.Cache["Post"] == null)
            {
                HttpContext.Cache["Post"] = DateTime.UtcNow.ToString();
            }
            Response.AddCacheItemDependency("Post");

            return View(posts);
        }

        [ChildActionOnly]
        public ActionResult Pager()
        {
            int count = 0;
            int pages = 1;
            using (GoodBlogDbContext db = new GoodBlogDbContext())
            {
                count = db.Posts.Count();
            }
            pages = (count / this.ItemsByPage) + 1;
            return PartialView("~/Views/Shared/_Pager.cshtml", new PagerModel() { Count = count, Pages = pages });
        }

        [HttpGet]
        public ActionResult Details(string slug)
        {
            Post post = new Post();
            using(GoodBlogDbContext db = new GoodBlogDbContext())
            {
                post = db.Posts
                    .Include(p => p.Comments)
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .Where(p => p.Slug == slug)
                    .FirstOrDefault();
            }

            return View(post);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comment(Comment newComment)
        {
            Post post = new Post();
            using (GoodBlogDbContext db = new GoodBlogDbContext())
            {
                post = db.Posts
                    .Include(p => p.Comments)
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .Where(p => p.Id == newComment.PostId)
                    .FirstOrDefault();
            }

            if(!this.ModelState.IsValid)
            {
                return View("Details", post);
            }
            using (GoodBlogDbContext db = new GoodBlogDbContext())
            {
                newComment.Created = DateTime.Now;
                db.Comments.Add(newComment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { slug = post.Slug });
        }
    }
}