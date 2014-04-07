using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Planit.Core
{
    public class Project
    {
       
        #region Fields & Properties 

            public int ID { get; set; }

            public string UserID { get; set; }

            public int Depth { get; set; }

            public int ParentID { get; set; }
            public string ParentTitle { get; set; } // commenting out until we need it
            [Required]
            public string Title { get; set; }
            public string Note { get; set; }
            public DateTime DueDate { get; set; }

            public DateTime StartDate { get; set; }

            public int Status { get; set; }

            public String ChildrenStr { get; private set; }

        #endregion 

        #region Constructors

            public Project()
            {
                ChildrenStr = "";
            }

            //public Project(int ID, string UserID, int Depth, string Title, DateTime  DueDate, DateTime StartDate, int Status)
            //{
            //    this.ID = ID;
            //    this.UserID = UserID;
            //    this.Depth = Depth;
            //    this.Title = Title;
            //    this.DueDate = DueDate;
            //    this.StartDate = StartDate;
            //    this.Status = Status;
            //    Children = new List<Project>();
            //}

            //public Project(int ID, string UserID, string Title, DateTime DueDate, DateTime StartDate, Project parent)
            //{
            //    this.ID = ID;
            //    this.UserID = UserID;
            //    this.Depth = parent.Depth + 1;
            //    this.Title = Title;
            //    this.DueDate = DueDate;
            //    this.StartDate = StartDate;
            //    Children = new List<Project>();
            //}

        #endregion

        #region Methods
            
            public Project addChild(Project child)
            {
                ChildrenStr = ChildrenStr + "," + child.ID;
                child.ParentID = this.ID;
                child.Depth = this.Depth + 1;
                return child;
            }
            
            
            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (StartDate == DueDate)
                {
                    yield return new ValidationResult
                     ("Start date must be before Due date", new[] { "StartDate", "DueDate" });
                }
            }
        #endregion


    }

    public class ProjectDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
    }

     
}