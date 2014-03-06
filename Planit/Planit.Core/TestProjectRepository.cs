using System;
using System.Collections.Generic;

namespace Planit.Core
{
    public class TestProjectRepository : IProjectRepository
    {
        public List<Project> GetProjects()
        {
            List<Project> projectList = new List<Project>();

            projectList.Add(new Project(001, "9999", 1, "", "Chores", new DateTime(),new DateTime(2014, 12, 31)));
            projectList.Add(new Project(002, "9999", 1, "", "School", new DateTime(),new DateTime(2014, 5, 10)));
            projectList.Add(new Project(003, "9999", 1, "", "Work", new DateTime(),new DateTime(2014, 12, 31)));
            
            //Chores >...
            projectList.Add(new Project(004, "9999", 2, "001", "Gardening", new DateTime(2014,04,01),new DateTime(2014, 5, 28)));
            projectList.Add(new Project(005, "9999", 2, "001", "Bills", new DateTime(),new DateTime(2014, 12, 30)));

            //Gardening > ...
            projectList.Add(new Project(006, "9999", 3, "004", "Gather Supplies", new DateTime(2014, 04,01),new DateTime(2014, 4, 10)));
            projectList.Add(new Project(007, "9999", 3, "004", "Prep", new DateTime(2014, 04, 11),new DateTime(2014, 04, 20)));
            projectList.Add(new Project(008, "9999", 3, "004", "Plant", new DateTime(2014, 04, 21),new DateTime(2014, 5, 01)));

            // School > ..
            projectList.Add(new Project(009, "9999", 2, "002", "CSC201j", new DateTime(),new DateTime(2014, 5, 10)));
            projectList.Add(new Project(010, "9999", 2, "002", "CSC202j", new DateTime(),new DateTime(2014, 5, 10)));

            // CSC201j > ..
            projectList.Add(new Project(011, "9999", 2, "009", "Reading", new DateTime(),new DateTime(2014, 5, 10)));
            projectList.Add(new Project(012, "9999", 2, "009", "Labs", new DateTime(),new DateTime(2014, 5, 10)));

            return projectList;

        }
    }
}
