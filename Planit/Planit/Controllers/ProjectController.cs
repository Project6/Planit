﻿using System;
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
           
            IEnumerable<Project> projects  = BAL.TraverseByDueDate();
            return View(projects);
        }

        // GET: /Project/
        public ActionResult Task()
        {
          //  ProjectBusinessLayer BAL = new ProjectBusinessLayer(new TestProjectDataLayer());
            IEnumerable<Project> tree = BAL.DFS();

            return View(tree);

        }

        // GET: /Project/
        public ActionResult Tree()
        {
           // ProjectBusinessLayer BAL = new ProjectBusinessLayer(new TestProjectDataLayer());
            IEnumerable<Project> tree = BAL.DFS();

            return View(tree);

        }

        // GET: /Project/
        public ActionResult Schedule()
        {
           // ProjectBusinessLayer BAL = new ProjectBusinessLayer(new TestProjectDataLayer());
            IEnumerable<Project> tree = BAL.DFS();// BAL.TraverseByDueDate();

            return View(tree);

        }

        // GET: /Project/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Project project = db.Projects.Find(id);
        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(project);
        //}

        // GET: /Project/Create
        public ActionResult Create()
        {
                return View();
        }

         //POST: /Project/Create
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,DueDate,StartDate")] Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BAL._DAL.db.Projects.Add(project);
                    BAL._DAL.db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
              }

         return View(project);
        }

        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
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

         //POST: /Project/Edit/5
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,DueDate,StartDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                BAL._DAL.db.Entry(project).State = EntityState.Modified;
                BAL._DAL.db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
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

         //POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = BAL._DAL.db.Projects.Find(id);
            BAL._DAL.db.Projects.Remove(project);
            BAL._DAL.db.SaveChanges();
            return RedirectToAction("Index");
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
