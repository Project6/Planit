using System.Collections.Generic;

namespace Planit.Core
{
    public interface IProjectRepository
    {
        List<Project> GetProjects();

        void AddProject(Project child, Project parent);
    }
}
