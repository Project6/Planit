using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Planit.Core
{
    public class ProjectBusinessLayer
    {
        #region Fields & Properties
            string _UserId;
            public ProjectDataLayer _DAL {get; private set;}
             
        #endregion

        #region Constructors
            
            public ProjectBusinessLayer(ProjectDataLayer DAL,string Userid)
            {
                _DAL = DAL;
                _UserId = Userid;
            }

        #endregion 

        #region Methods
        
            /// <summary>
            ///     Returns an <see cref="IQueryable"/> of <see cref="Project"/>s for the current user.
            /// </summary>
            /// <returns>
            ///     Returns an <see cref="IQueryable"/> collection of <see cref="Project"/>'s.
            /// </returns>
            private IQueryable<Project> GetUserProjects()
            {
                var projects = from m in _DAL.GetProjects()
                               where m.UserID == _UserId
                                    &&
                                    m.Status < 100
                               select m;
                return projects;
            }

            private IEnumerable<Project> GetLeafNodeProjects()
            {
                var projects = GetUserProjects().ToList().Where(m => !Regex.IsMatch(m.ChildrenStr, @"\d"));
                return projects;
            }
            
            /// <summary>
        ///     Performs a depth first search of the Root Node.   
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerable"/> as a results of a depth first search of the Root Node.
        /// </returns>
            public IEnumerable<Project> DFS()
            {
                return DFS(_DAL.Root);
            }

            /// <summary>
            ///     Performs a depth first search of <paramref name="parent"/>
            /// </summary>
            /// <param name="parent">parent</param>
            /// <returns>
            ///     An <see cref="IEnumerable"/> as a results of a depth first search of <paramref name="parent"/>.
            /// </returns>
            public IEnumerable<Project> DFS(Project parent)
            {
                if(parent.Status < 100)
                    yield return parent;

                foreach (var child in getChildren(parent))
                {
                    foreach (var grandchild in DFS(child))
                    {
                        if(grandchild.Status < 100)
                            yield return grandchild;
                    }
                }
            }
            
            /// <summary>
            ///     Performs a traversal by the <see cref="Project"/>'s <c>DueDate</c> field.
            /// </summary>
            /// <returns>
            ///     An <see cref="IQueryable"/> as a results of a <c>DueDate</c> traversal.
            /// </returns>
            public IEnumerable<Project> TraverseByDueDate()
            {
                var projects = from m in GetLeafNodeProjects()
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
                Project result;
                foreach(string childStr in ChildrenStrArray)
                {
                    int childInt;
                    if (int.TryParse(childStr, out childInt))
                    {
                        result = _DAL.Find(childInt);
                        if (result != null)
                        {
                            yield return result;
                        }
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

            public void Edit(Project project)
            {
                _DAL.Edit(project);
            }

            public Project Remove(Project project)
            {
                // if root node
                if (project == _DAL.Root)
                    return _DAL.Remove(project);

                Project parent = Find(project.ParentID);
                parent.removeChildRef(project, parent);
                _DAL.Update(parent);
                List<Project> list = new List<Project>();

                foreach (Project p in DFS(project))
                {
                    list.Add(p);
                }

                foreach (Project p in list)
                {
                    _DAL.Remove(p);
                }

                return project;
            }

            public Project Complete(Project project)
            {
                List<Project> list = new List<Project>();

                foreach (Project p in DFS(project))
                {
                    list.Add(p);
                }

                foreach (Project p in list)
                {
                    p.Status = 100;
                    _DAL.Edit(p);
                }
                return project;
            }
        
        #endregion


    }

}
