using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planit.Core
{
    public class ProjectBusiness
    {

        IProjectRepository _repository;

        public ProjectBusiness(IProjectRepository repository)
        {
            _repository = repository;
        }

        public List<Project> GetProjects()
        {
            return _repository.GetProjects();
        }

        public IEnumerable<Project> dfs(Project parent)
        {
            foreach (var child in parent.Children)
            {
                yield return child;
                foreach (var grandchild in dfs(child))
                {
                    yield return grandchild;
                }
            }
        }

        public void addChild(Project child, Project parent)
        {
            parent.Children.Add(child);
        }
    }
}
