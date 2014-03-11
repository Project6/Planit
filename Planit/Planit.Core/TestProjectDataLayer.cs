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
            Project Chores = new Project { Description = "Chores", Depth = 1 };
            Project Work = new Project { Description = "Work", Depth = 1 };
            Project School = new Project { Description = "School", Depth = 1 };

            Chores = Root.addChild(Chores);
            School = Root.addChild(School);
            Work = Root.addChild(Work);

            Project Gardening = new Project { Description = "Gardening", Depth = 2 };
            Project Bills = new Project { Description = "Bills", Depth = 2 };

            Gardening = Chores.addChild(Gardening);
            Bills = Chores.addChild(Bills);

            Project CSC201j = new Project { Description = "CSC201j", Depth = 2 };
            Project CSC202j = new Project { Description = "CSC202j", Depth = 2 };

            CSC201j = School.addChild(CSC201j);
            CSC202j = School.addChild(CSC202j);

            Project Prep = new Project { Description = "Prep", Depth = 3 };
            Project Gathering = new Project { Description = "Gathering Supplies", Depth = 3 };
            Project Planting = new Project { Description = "Planting", Depth = 3 };
            Project Mulch = new Project { Description = "Mulch", Depth = 3 };

            Prep = Gardening.addChild(Prep);
            Gathering = Gardening.addChild(Gathering);
            Planting = Gardening.addChild(Planting);
            Mulch = Gardening.addChild(Mulch);

            Project Reading = new Project { Description = "Reading", Depth = 3 };
            Project Labs = new Project { Description = "Labs", Depth = 3 };

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
