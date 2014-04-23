using System;
using System.Data.Entity;
using System.Linq;


namespace Planit.Core
{
    public class ProjectDataLayer : IProjectDataLayer
    {
        public Project Root { get; private set; }
        public ProjectDbContext db = new ProjectDbContext("ProjectDB");
       
        public ProjectDataLayer(string Userid, string UserName)
        {
            try
            {
                Root = db.Projects
                                    .Where(p => p.Depth == 0)
                                    .Where(u=> u.UserID == Userid)
                                    .Select(p => p)
                                    .First<Project>();
            }
            catch (Exception)
            {
                Root = new Project() {  Title = UserName + "\'s Tasks", 
                                        Depth = 0, 
                                        DueDate = new DateTime(2014, 4, 15), 
                                        StartDate = new DateTime(2014, 4, 15), 
                                        Status = 0, 
                                        ParentID = 0 , 
                                        UserID = Userid};
                Root = Add(Root);
            }
        }

        public IQueryable<Project> GetProjects()
        {
            var projects = from m in db.Projects
                            select m;
            return projects;
        }
        
        public Project Add(Project project)
        {
            Project returnedProject = db.Projects.Add(project);
            db.SaveChanges();
            return returnedProject;
        }

        public void Edit(Project project)
        {
            //Project projectOld = Find(project.ID);
            //db.Entry(projectOld).CurrentValues.SetValues(project);

            db.Entry(project).State = EntityState.Modified;
            db.Entry(project).Property(proj => proj.UserID).IsModified = false;
            db.Entry(project).Property(proj => proj.ChildrenStr).IsModified = false;
            db.Entry(project).Property(proj => proj.Depth).IsModified = false;
            db.Entry(project).Property(proj => proj.ParentID).IsModified = false;
            db.Entry(project).Property(proj => proj.ParentTitle).IsModified = false;

            db.SaveChanges();
        }

        public void Update(Project project)
        {
            Project projectOld = Find(project.ID);
            db.Entry(projectOld).CurrentValues.SetValues(project);
            db.SaveChanges();
        }

        public Project Find(int? id)
        {
            return db.Projects.Find(id);
        }
        
        public Project Remove(Project project)
        {
            Project removed = db.Projects.Remove(project);
            db.SaveChanges();
            return removed;
           
        }

        public void Seed1()
        {
            using (db)
            {

                Project Chores = new Project
                {
                    ID = 10,
                    Title = "eChores_1",
                    Depth = 1,
                    DueDate = new DateTime(2014, 4, 15),
                    StartDate = new DateTime(2014, 2, 25),
                    Status = 10
                };

                Project Gardening = new Project
                {
                    ID = 11,
                    Title = "fGardening_2",
                    Depth = 2,
                    DueDate = new DateTime(2014, 3, 15),
                    StartDate = new DateTime(2014, 3, 5),
                    Status = 30
                };
                Chores = Root.addChild(Chores);
                Gardening = Chores.addChild(Gardening);

                db.Projects.Add(Root);
                db.SaveChanges();
            }
        }
        public void Seed()
        {
            Project Chores = new Project
            { 
                ID = 10,
                Title = "Chores_1",
                Depth = 1,
                DueDate = new DateTime(2014, 5, 15),
                StartDate = new DateTime(2014, 2, 25)

            };

            Chores = Root.addChild(Chores);

            Project Gardening = new Project
            {
                ID = 11,
                Title = "Gardening_2",
                Depth = 2,
                DueDate = new DateTime(2014, 5, 15),
                StartDate = new DateTime(2014, 3, 5)
            };
            Gardening = Chores.addChild(Gardening);

            Project Prep = new Project
            {
                ID = 12,
                Title = "Prep_3",
                Depth = 3,
                DueDate = new DateTime(2014, 3, 15),
                StartDate = new DateTime(2014, 2, 25)
            };
            Prep = Gardening.addChild(Prep);

            Project Gathering = new Project
            {
                ID = 13,
                Title = "Gathering Supplies_3",
                Depth = 3,
                DueDate = new DateTime(2014, 3, 15),
                StartDate = new DateTime(2014, 3, 5)
            };
            Gathering = Gardening.addChild(Gathering);

            Project Planting = new Project
            {
                ID = 14,
                Title = "Planting_3",
                Depth = 3,
                DueDate = new DateTime(2014, 5, 15),
                StartDate = new DateTime(2014, 4, 15)
            };
            Planting = Gardening.addChild(Planting);

            Project Mulch = new Project
            {
                ID = 15,
                Title = "Mulch_3",
                Depth = 3,
                DueDate = new DateTime(2014, 5, 15),
                StartDate = new DateTime(2014, 5, 5)
            };
            Mulch = Gardening.addChild(Mulch);

            Project Bills = new Project
            {
                ID = 16,
                Title = "Bills_2",
                Depth = 2,
                DueDate = new DateTime(2014, 4, 15),
                StartDate = new DateTime(2014, 4, 2)
            };
            Bills = Chores.addChild(Bills);

            Project Work = new Project
            {
                ID = 17,
                Title = "Work_1",
                Depth = 1,
                DueDate = new DateTime(2014, 4, 10),
                StartDate = new DateTime(2014, 3, 4)
            };
            Work = Root.addChild(Work);

            Project School = new Project
            {
                ID = 18,
                Title = "School_1",
                Depth = 1,
                DueDate = new DateTime(2014, 5, 1),
                StartDate = new DateTime(2014, 2, 25)
            };
            School = Root.addChild(School);

            Project CSC201j = new Project
            {
                ID = 19,
                Title = "CSC201j_2",
                Depth = 2,
                DueDate = new DateTime(2014, 5, 1),
                StartDate = new DateTime(2014, 2, 25)
            };
            CSC201j = School.addChild(CSC201j);

            Project CSC202j = new Project
            {
                ID = 20,
                Title = "CSC202j_2",
                Depth = 2,
                DueDate = new DateTime(2014, 5, 1),
                StartDate = new DateTime(2014, 2, 25)
            };
            CSC202j = School.addChild(CSC202j);

            Project Reading = new Project
            {
                ID = 21,
                Title = "Reading_3",
                Depth = 3,
                DueDate = new DateTime(2014, 3, 20),
                StartDate = new DateTime(2014, 3, 10)
            };
            Project Labs = new Project
            {
                ID = 22,
                Title = "Labs_3",
                Depth = 3,
                DueDate = new DateTime(2014, 5, 1),
                StartDate = new DateTime(2014, 2, 25)
            };

            Reading = CSC201j.addChild(Reading);
            Labs = CSC201j.addChild(Labs);

            db.Projects.Add(Root);
            db.SaveChanges();

        }

        public void SeedRoot()
        {
            Root = new Project() { Title = "Devin's Task's", Depth = 0, DueDate = new DateTime(2020, 01, 01), StartDate = new DateTime(2014, 3, 31) };
        }
        
    }
}
