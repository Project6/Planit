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

            IEnumerable<Project> list = BAL.TraverseByDueDate();

            foreach (var item in list)
            {
                Console.WriteLine(item.Description + "\n\t\t" + item.DueDate);
            }
            // commented out to use (fn & ctrl) + f5 to run and keep console open
            //Console.ReadLine();
        }
    }
}
