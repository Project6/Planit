using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Planit.Core;

namespace Planit.Controllers
{
    public class ProjectController : Controller
    {
        //private ProjectDBContext db = new ProjectDBContext();
        ProjectBusinessLayer BAL = new ProjectBusinessLayer(new ProjectDataLayer());
        // GET: /Project/

        public ActionResult Outline()
        {

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
            Project project = BAL._DAL.db.Projects.Find(id);
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
        public ActionResult Create([Bind(Include = "ID,Title,DueDate,StartDate,Status,ParentID,Depth,ParentTitle")] Project project, string returnUrl, int? id)
        {
            Project parent;
            if (id != null)
            {
                parent = BAL._DAL.db.Projects.Find(id); //this is the project that create was clicked on
            }
            else
            {
                parent = new Project() { Title = "Your Task's", Depth = 0, DueDate = new DateTime(2014, 4, 15), StartDate = new DateTime(2014, 4, 15), Status = 0 };
            }

            try
            {
                if (ModelState.IsValid)
                {
                    project = parent.addChild(project); //add the new project as a child of the originating project
                    BAL._DAL.db.Projects.Add(project);
                    BAL._DAL.db.SaveChanges();
                    return Redirect(returnUrl);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return View(project);
        }

        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.returnUrl = Request.UrlReferrer;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = BAL._DAL.db.Projects.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID,Title,DueDate,StartDate,Status,ParentID,Depth,ParentTitle")] Project project, string returnUrl)
        {
            var v = BAL._DAL.db.Projects.Find(project.ID);
            try
            {
                if (ModelState.IsValid)
                {
                    //BAL._DAL.db.Entry(project).State = EntityState.Modified;
                    BAL._DAL.db.Entry(v).CurrentValues.SetValues(project);
                    BAL._DAL.db.SaveChanges();
                    return Redirect(returnUrl);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(project);
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.returnUrl = Request.UrlReferrer;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = BAL._DAL.db.Projects.Find(id);
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
            Project project = BAL._DAL.db.Projects.Find(id);
            BAL._DAL.db.Projects.Remove(project);
            BAL._DAL.db.SaveChanges();
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
    }
}
