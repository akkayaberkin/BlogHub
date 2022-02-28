using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Entities;
using Blog.BusinessLayer;
using Blog.BusinessLayer.Results;
using Blog.WebApp.Filters;
using Blog.WebApp.Models;
using Blog.Entities.Messages;

namespace Blog.WebApp.Controllers
{
    [Auth]
    [AuthAdmin]
    [Exc]
    public class BlogUserController : Controller
    {
        private BlogUserManager BlogUserManager = new BlogUserManager();


        public ActionResult Index()
        {
            return View(BlogUserManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogUser BlogUser = BlogUserManager.Find(x => x.Id == id.Value);

            if (BlogUser == null)
            {
                return HttpNotFound();
            }

            return View(BlogUser);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogUser BlogUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogUser> res = BlogUserManager.Insert(BlogUser);

                if(res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(BlogUser);
                }

                return RedirectToAction("Index");
            }

            return View(BlogUser);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogUser BlogUser = BlogUserManager.Find(x => x.Id == id.Value);

            if (BlogUser == null)
            {
                return HttpNotFound();
            }

            return View(BlogUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogUser BlogUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogUser> res = BlogUserManager.Update(BlogUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(BlogUser);
                }

                return RedirectToAction("Index");
            }
            return View(BlogUser);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogUser BlogUser = BlogUserManager.Find(x => x.Id == id.Value);

            if (BlogUser == null)
            {
                return HttpNotFound();
            }

            return View(BlogUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessLayerResult<BlogUser> res = new BusinessLayerResult<BlogUser>();

            BlogUser BlogUser = BlogUserManager.Find(x => x.Id == id);
            if (BlogUser.Id == CurrentSession.User.Id)
            {
                res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemez.");
                return RedirectToAction("Index");
            }
            BlogUserManager.Delete(BlogUser);

            return RedirectToAction("Index");
        }
    }
}
