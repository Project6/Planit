using System.Collections.Generic;

namespace Planit.Core
{
    public interface IProjectRepository
    {
        // Possibly a Head/root field so you always have the top of the tree

        List<Project> GetProjects();

    }
}
