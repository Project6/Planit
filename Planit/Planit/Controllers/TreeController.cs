using Planit.Core;
//using Planit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planit.Controllers
{
    public class TreeController : Controller
    {
        //Node<Project> root = new Node<Project>( new Project{Description = "Devin's Tasks"});


        void Seed()
        { 
                root.addChild(new Project { Description = "Chores" });
                root.addChild(new Project { Description = "School" });
                root.addChild(new Project { Description = "Work" });

                    LinkedList<Node<Project>>.Enumerator rootEnumerator = root.Children.GetEnumerator();
                    rootEnumerator.MoveNext();
                    Node<Project> chores = rootEnumerator.Current;
                        chores.addChild(new Project { Description = "Gardening" });
                        //chores.addChild(new Project { Description = "Clean" });

                        LinkedList<Node<Project>>.Enumerator choresEnumerator = chores.Children.GetEnumerator();
                        choresEnumerator.MoveNext();
                        Node<Project> gardening = choresEnumerator.Current;

                            gardening.addChild(new Project { Description = "Gather Supplies" });
                            gardening.addChild(new Project { Description = "Prep" });
                            gardening.addChild(new Project { Description = "Plant" });
                            gardening.addChild(new Project { Description = "Mulch" });

                

        }
        //
        // GET: /Tree/
        public ActionResult Index()
        {
            //IEnumerable<Node<Project>> test = root.dfs(root); // testing to make sure if root has no children it doesn't crash
            //Seed();
            ProjectBusinessLayer BAL = new ProjectBusinessLayer(new TestProjectDataLayer());
            IEnumerable<Project> tree = BAL.DFS();
            
            return View(tree);
        }

        //
        // GET: /Tree/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Tree/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tree/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tree/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Tree/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tree/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tree/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
