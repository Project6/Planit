﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planit.Core
{
    public class ProjectBusinessLayer
    {

        public IProjectDataLayer _DAL {get; private set;}

        public ProjectBusinessLayer(IProjectDataLayer DAL)
        {
            _DAL = DAL;
        }

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

        //public void Find(Project project)
        //{ 
            
        //}
    }
}
