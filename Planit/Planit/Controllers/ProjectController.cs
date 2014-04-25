using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Planit.Core;
using Microsoft.AspNet.Identity;

namespace Planit.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        ProjectBusinessLayer BAL;
        public ProjectController()
        {
           string User = System.Web.HttpContext.Current.User.Identity.GetUserId();
           string UserName = System.Web.HttpContext.Current.User.Identity.GetUserName();
           BAL = new ProjectBusinessLayer(new ProjectDataLayer(User, UserName), User);
        }

        // GET: /Project/

        public ActionResult Outline()
        {
            ViewBag.BAL = BAL;
            IEnumerable<Project> projects = BAL.DFS();
            return View(projects);
        }

        public ActionResult _Outline()
        {

            IEnumerable<Project> projects = BAL.TraverseByDueDate();
            return View(projects);
        }

        // GET: /Project/
        public ActionResult Task(string sortOrder)
        {
            ViewBag.BAL = BAL;
            IEnumerable<Project> projects = BAL.TraverseByStartDate();
            ViewBag.DueDateSortParm = String.IsNullOrEmpty(sortOrder) ? "DueDate_desc" : "DueDate";
            ViewBag.DueDateSortParm = sortOrder == "DueDate" ? "DueDate_desc" : "DueDate";
            ViewBag.StartDateSortParm = sortOrder == "StartDate" ? "StartDate_desc" : "StartDate";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "Status_desc" : "Status";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";

            switch (sortOrder)
            {
                case "DueDate_desc":
                    projects = projects.OrderByDescending(p => p.DueDate);
                    break;
                case "DueDate":
                    projects = projects.OrderBy(p => p.DueDate);
                    break;
                case "StartDate":
                    projects = projects.OrderBy(p => p.StartDate);
                    break;
                case "Status_desc":
                    projects = projects.OrderByDescending(p => p.Status);
                    break;
                case "Status":
                    projects = projects.OrderBy(p => p.Status);
                    break;
                case "Title_desc":
                    projects = projects.OrderByDescending(p => p.Title);
                    break;
                case "Title":
                    projects = projects.OrderBy(p => p.Title);
                    break;
                default:
                    projects = projects.OrderBy(p => p.DueDate);
                    break;
            }
            return View(projects.ToList());

        }

        // GET: /Project/
        public ActionResult Tree()
        {
            IEnumerable<Project> tree = BAL.DFS();
            return View(tree);

        }

        // GET: /Project/
        public ActionResult Schedule()
        {
            IEnumerable<Project> schedule = BAL.TraverseByDueDate();
            return View(schedule);
        }

        // GET: /Project/
        public ActionResult Dashboard()
        {
            IEnumerable<Project> dashboard = BAL.TraverseByDueDate();
            return View(dashboard);
        }

        // GET: /Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = BAL.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        

        // GET: /Project/Create
        public ActionResult Create()
        {
            ViewBag.returnUrl = Request.UrlReferrer;
            return View();
        }

        //POST: /Project/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Depth,ParentID,ParentTitle,Title,Note,DueDate,StartDate,Status,ChildrenStr")] Project child, string returnUrl, int? id)
        {
            Project parent = BAL.Find(id); //this is the project that create was clicked on

            try
            {
                if (ModelState.IsValid)
                {
                    //child.ParentTitle = parent.Title;
                    child = BAL.AddChild(child, parent);
                    return Redirect(returnUrl);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return View(child);
        }

        
        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.returnUrl = Request.UrlReferrer;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = BAL.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //POST: /Project/Edit/5
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Depth,ParentID,ParentTitle,Title,Note,DueDate,StartDate,Status,ChildrenStr")] Project project, string returnUrl)
        {
            project = BAL.Find(project.ID);
            try
            {
                if (ModelState.IsValid)
                {
                    BAL.Edit(project);
                    return Redirect(returnUrl);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(project);
        }

        // GET: /Project/Complete/
        public ActionResult Complete(int? id)
        {
            ViewBag.returnUrl = Request.UrlReferrer;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = BAL.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //POST: /Project/Complete/
        [HttpPost, ActionName("Complete")]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteConfirmed(int id, string returnUrl)
        {
            BAL.Complete(BAL.Find(id));
            return Redirect(returnUrl);
        }



        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.returnUrl = Request.UrlReferrer;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = BAL.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string returnUrl)
        {
            BAL.Remove( BAL.Find(id) );
            return Redirect(returnUrl);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public ActionResult Analytics()
        {
            return View();
        }

        public ActionResult Notifications()
        {
            return View();
        }
    }
}
