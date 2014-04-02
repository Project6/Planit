 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planit.Core
{
    public class ProjectBusinessLayer
    {
        #region Fields & Properties

            public ProjectDataLayer _DAL {get; private set;}
            public IEnumerable<Project> _Tree { get; private set; }
          //  public List<Project> _List { get; private set; }
        
        #endregion

        #region Constructors
            
            public ProjectBusinessLayer(ProjectDataLayer DAL)
            {
                _DAL = DAL;
                _Tree = DFS();
               // _List = DAL.db.Projects.ToList<Project>();
            }

        #endregion 

        #region Methods
        
            public IEnumerable<Project> DFS()
            {
                DFS(_DAL.Root);
                return DFS(_DAL.Root);
            }

            public IEnumerable<Project> DFS(Project parent)
            {
                foreach (var child in parent.Children)
                {
                    yield return child;
                    foreach (var grandchild in DFS(child))
                    {
                        yield return grandchild;
                    }
                }
            }
            public IQueryable<Project> TraverseByDueDate()
            {
                var projects = from m in _DAL.db.Projects
                               orderby m.DueDate
                               select m;
                return projects;
            }


            public IQueryable<Project> TraverseByStartDate()
            {
                var projects = from m in _DAL.db.Projects
                               orderby m.StartDate
                               select m;
                return projects;
            }

            //public List<Project> GetProjects()
            //{
            //    return _repository.GetProjects();
            //}
            //
            //public void AddChild(Project child, Project parent)
            //{
            //    parent.Children.Add(child);
            //}

            //public void Remove(Project project)
            //{ 
            //    // remove
            //} 
        
        #endregion
    }
}
