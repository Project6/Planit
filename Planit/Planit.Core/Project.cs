﻿using System;
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

            public List<Project> Children { get; private set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) 
     { 
         if (StartDate == DueDate) 
         { 
             yield return new ValidationResult 
              ("Start date must be before Due date", new[] { "StartDate", "DueDate" }); 
         } 
     } 

        #endregion 

        #region Constructors

            public Project()
            {
                Children = new List<Project>();
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

            public Project(Project child, Project parent)
            {
                this.ID = child.ID;
                this.UserID = child.UserID;
                this.Depth = parent.Depth + 1;
                this.Title = child.Title;
                this.DueDate = child.DueDate;
                this.StartDate = child.StartDate;
                this.Status = child.Status;
                this.ParentTitle = parent.Title;
               
                Children = new List<Project>();
            }

        #endregion

            // add's Project argument to the children list and then returns a reference to the child
            public Project addChild(Project child)
            {
                Children.Add(new Project(child, this));
                return Children.Last();
            }


          
    }

    public class ProjectDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
    }

     
}