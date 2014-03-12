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

            Console.WriteLine( BAL.Find(11).Description ); 

            //IEnumerable<Project> list = BAL.TraverseByDueDate();

            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.Description + "\n\t\t" + item.DueDate);
            //}

        }
    }
}
