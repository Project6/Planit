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
                yield return parent;

                foreach (var child in getChildren(parent))
                {
                    //yield return child;
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
            
            // Adds Child to the DB and updates relationship
            public Project AddChild(Project child, Project parent)
            {
                child = _DAL.Add(child); // Generates DB ID for child
                child = parent.addChild(child); // adds id to parent.Children AND assigns child.ParentID to parent.ID
                _DAL.Update(child); // updates child db reference with new ParentID
                _DAL.Update(parent); // updates parent db reference with new children string
                return child;
            }
            public IEnumerable<Project> getChildren(Project parent)
            {    
                string[] ChildrenStrArray = parent.ChildrenStr.Split(',');
                foreach(string childStr in ChildrenStrArray)
                {
                    int childInt;
                    if (int.TryParse(childStr, out childInt))
                    {
                        yield return _DAL.Find(childInt);
                    }

                   
                }
            }
            public Project Find(int? id)
            {
                return _DAL.Find(id);
            }
            public void Update(Project project)
            {
                _DAL.Update(project);
            }
            public Project Remove(Project project)
            {
                return _DAL.Remove(project);
            }

            

            //public List<Project> GetProjects()
            //{
            //    return _repository.GetProjects();
            //}
            //
            //public void Remove(Project project)
            //{ 
            //    // remove
            //} 
        
        #endregion
    }
}
