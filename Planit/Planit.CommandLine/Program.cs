using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planit.Core;
using System.Data.Entity;

namespace Planit.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProjectBusinessLayer BAL = new ProjectBusinessLayer(new TestProjectDataLayer());

            //Console.WriteLine( BAL.Find(11).Description );


            using (ProjectDbContext db = new ProjectDbContext())
            {
                Project Chores = new Project
                {
                    ID = 10,
                    Description = "Chores_1",
                    Depth = 1,
                    DueDate = new DateTime(2014, 5, 15),
                    StartDate = new DateTime(2014, 2, 25), 
                };

                Project Gardening = new Project
                {
                    ID = 11,
                    Description = "Gardening_2",
                    Depth = 2,
                    DueDate = new DateTime(2014, 5, 15),
                    StartDate = new DateTime(2014, 3, 5)
                };

                Gardening = Chores.addChild(Gardening);

                db.Projects.Add(Chores);
                db.SaveChanges();

                // Display all Blogs from the database 
                var query = from b in db.Projects
                            orderby b.Description
                            select b;

                foreach (var item in query)
                {
                    Console.WriteLine(item.Description);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

            //IEnumerable<Project> list = BAL.TraverseByDueDate();

            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.Description + "\n\t\t" + item.DueDate);
            //}

        }
    }
}
