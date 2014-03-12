using System;
using System.Collections.Generic;

namespace Planit.Core
{
    public class TestProjectDataLayer : IProjectDataLayer
    {
        public Project Root { get; private set; }

        public TestProjectDataLayer()
        {
            // need to set by acessing DB
            Root = new Project() { Description = "Devin's Task's", Depth = 0 };

            Seed();
        }

        public void Seed()
        {
            Project Chores = new Project
            {
 
 
                ID = 10,
                Description = "Chores_1",
                Depth = 1,
                DueDate = new DateTime(2014, 5, 15),
                StartDate = new DateTime(2014, 2, 25)
            };

            Chores = Root.addChild(Chores);

            Project Gardening = new Project
            {
 
 
                ID = 11,
                Description = "Gardening_2",
                Depth = 2,
                DueDate = new DateTime(2014, 5, 15),
                StartDate = new DateTime(2014, 3, 5)
            };
            Gardening = Chores.addChild(Gardening);

            Project Prep = new Project
            {
 
 
                ID = 12,
                Description = "Prep_3",
                Depth = 3,
                DueDate = new DateTime(2014, 3, 15),
                StartDate = new DateTime(2014, 2, 25)
            };
            Prep = Gardening.addChild(Prep);

            Project Gathering = new Project
            {
 
 
                ID = 13,
                Description = "Gathering Supplies_3",
                Depth = 3,
                DueDate = new DateTime(2014, 3, 15),
                StartDate = new DateTime(2014, 3, 5)
            };
            Gathering = Gardening.addChild(Gathering);

            Project Planting = new Project
            {
 
 
                ID = 14,
                Description = "Planting_3",
                Depth = 3,
                DueDate = new DateTime(2014, 5, 15),
                StartDate = new DateTime(2014, 4, 15)
            };
            Planting = Gardening.addChild(Planting);

            Project Mulch = new Project
            {
 
 
                ID = 15,
                Description = "Mulch_3",
                Depth = 3,
                DueDate = new DateTime(2014, 5, 15),
                StartDate = new DateTime(2014, 5, 5)
            };
            Mulch = Gardening.addChild(Mulch);

            Project Bills = new Project
            {
 
 
                ID = 16,
                Description = "Bills_2",
                Depth = 2,
                DueDate = new DateTime(2014, 4, 15),
                StartDate = new DateTime(2014, 4, 2)
            };
            Bills = Chores.addChild(Bills);

            Project Work = new Project
            {
 
 
                ID = 17,
                Description = "Work_1",
                Depth = 1,
                DueDate = new DateTime(2014, 4, 10),
                StartDate = new DateTime(2014, 3, 4)
            };
            Work = Root.addChild(Work);

            Project School = new Project
            {
 
 
                ID = 18,
                Description = "School_1",
                Depth = 1,
                DueDate = new DateTime(2014, 5, 1),
                StartDate = new DateTime(2014, 2, 25)
            };
            School = Root.addChild(School);

            Project CSC201j = new Project
            {
 
 
                ID = 19,
                Description = "CSC201j_2",
                Depth = 2,
                DueDate = new DateTime(2014, 5, 1),
                StartDate = new DateTime(2014, 2, 25)
            };
            CSC201j = School.addChild(CSC201j);

            Project CSC202j = new Project
            {
 
 
                ID = 20,
                Description = "CSC202j_2",
                Depth = 2,
                DueDate = new DateTime(2014, 5, 1),
                StartDate = new DateTime(2014, 2, 25)
            };
            CSC202j = School.addChild(CSC202j);

            Project Reading = new Project
            {
 
 
                ID = 21,
                Description = "Reading_3",
                Depth = 3,
                DueDate = new DateTime(2014, 3, 20),
                StartDate = new DateTime(2014, 3, 10)
            };
            Project Labs = new Project
            {
 
 
                ID = 22,
                Description = "Labs_3",
                Depth = 3,
                DueDate = new DateTime(2014, 5, 1),
                StartDate = new DateTime(2014, 2, 25)
            };

            Reading = CSC201j.addChild(Reading);
            Labs = CSC201j.addChild(Labs);




        }


        //public List<Project> GetProjects()
        //{
        //    List<Project> projectList = new List<Project>();

            

        //    return projectList;

        //}

            //projectList.Add(new Project(001, "9999", 1, "Chores", new DateTime(),new DateTime(2014, 12, 31)));
            //projectList.Add(new Project(002, "9999", 1, "School", new DateTime(),new DateTime(2014, 5, 10)));
            //projectList.Add(new Project(003, "9999", 1, "Work", new DateTime(),new DateTime(2014, 12, 31)));
            
            ////Chores >...
            //projectList.Add(new Project(004, "9999", 2, "Gardening", new DateTime(2014,04,01),new DateTime(2014, 5, 28)));
            //projectList.Add(new Project(005, "9999", 2, "Bills", new DateTime(),new DateTime(2014, 12, 30)));

            ////Gardening > ...
            //projectList.Add(new Project(006, "9999", 3, "Gather Supplies", new DateTime(2014, 04,01),new DateTime(2014, 4, 10)));
            //projectList.Add(new Project(007, "9999", 3, "Prep", new DateTime(2014, 04, 11),new DateTime(2014, 04, 20)));
            //projectList.Add(new Project(008, "9999", 3, "Plant", new DateTime(2014, 04, 21),new DateTime(2014, 5, 01)));

            //// School > ..
            //projectList.Add(new Project(009, "9999", 2, "CSC201j", new DateTime(),new DateTime(2014, 5, 10)));
            //projectList.Add(new Project(010, "9999", 2, "CSC202j", new DateTime(),new DateTime(2014, 5, 10)));

            //// CSC201j > ..
            //projectList.Add(new Project(011, "9999", 2, "Reading", new DateTime(),new DateTime(2014, 5, 10)));
            //projectList.Add(new Project(012, "9999", 2, "Labs", new DateTime(),new DateTime(2014, 5, 10
    }
}
