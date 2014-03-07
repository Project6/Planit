using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planit.Core;

namespace Planit.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectBusinessLayer BAL = new ProjectBusinessLayer(new TestProjectDataLayer());

            foreach (Project project in BAL.DFS())
            {
                for(int i = 0; project.Depth > i; i++ )
                    Console.Write("-");
                
                Console.WriteLine(project.Description);
            }

        }
    }
}
