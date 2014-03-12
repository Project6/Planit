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

            public IProjectDataLayer _DAL {get; private set;}
            public IEnumerable<Project> _Tree { get; private set; }
            public List<Project> _List { get; private set; }
        
        #endregion

        #region Constructors
            
            public ProjectBusinessLayer(IProjectDataLayer DAL)
            {
                _DAL = DAL;
                _Tree = DFS();
                _List = _Tree.ToList<Project>();
            }

        #endregion 

        #region Methods
        
            public IEnumerable<Project> DFS()
            {
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

            public IEnumerable<Project> TraverseByDueDate()
            {
                return TraverseByDueDate(_DAL.Root);
            }

            public IEnumerable<Project> TraverseByDueDate(Project parent)
            {
                _List.Sort( (first, second) => first.DueDate.CompareTo(second.DueDate) >= 0 ? 1 : -1);
                return _List;
            }

            public Project Find(int id)
            {
                var results = _Tree.Where<Project>(project => project.ID == id);
                return results.First();
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
