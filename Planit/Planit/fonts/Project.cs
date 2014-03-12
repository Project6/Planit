using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

// REFERENCE Project from Planit.Core

namespace Planit.Models
{
    public class Project

    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public int Depth { get; set; }
        
        public string ParentID { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime StartDate { get; set; }
    }
    public class ProjectDBContext : ApplicationDbContext {
        public DbSet<Project> Projects { get; set; }
    }
}