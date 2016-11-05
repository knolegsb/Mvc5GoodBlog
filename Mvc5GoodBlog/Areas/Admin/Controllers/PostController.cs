using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Mvc5GoodBlog.ViewModels;
using System.Net;

namespace Mvc5GoodBlog.Areas.Area.Controllers
{
    public class PostController : Controller
    {
        private GoodBlogDbContext db = new GoodBlogDbContext();
        public int ItemsByPage = 5;

        // GET: Post
        public ActionResult Index(int page = 1)
        {
            int startIndex = page <= 1 ? 0 : (page - 1) * this.ItemsByPage;

            var posts = db.Posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .OrderByDescending(p => p.Created)
                .Skip(startIndex)
                .Take(this.ItemsByPage);

            return View(posts.ToList());
        }

        [ChildActionOnly]
        public ActionResult Pager()
        {
            int count = 0;
            int pages = 1;
            count = db.Posts.Count();
            pages = (count / this.ItemsByPage) + 1;

            return PartialView("~/Views/Shared/_Pager.cshtml", new PagerModel() { Count = count, Pages = pages });
        }

        // GET: /Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name");
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: /Admin/Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, CategoryId, UserId, Name, Slug, Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Created = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", post.CategoryId);
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Username", post.UserId);

            HttpContext.Cache.Remove("Post");
            return View(post);
        }

        // GET: /Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", post.CategoryId);
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Username", post.UserId);
            return View(post);
        }

        // POST: /Admin/Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, CategoryId, UserId, Name, Slug, Content, Created")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", post.CategoryId);
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Username", post.UserId);

            HttpContext.Cache.Remove("Post");

            return View(post);
        }

        // POST: /Admin/Post/Delete/5
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);

            foreach (Comment comment in post.Comments.ToList())
            {
                db.Comments.Remove(comment);
            }
            db.SaveChanges();

            db.Posts.Remove(post);
            db.SaveChanges();

            HttpContext.Cache.Remove("Post");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}