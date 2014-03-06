using System.Collections.Generic;

namespace Planit.Core
{
    interface IProjectRepository
    {
        List<Project> GetProjects();
    }
}
