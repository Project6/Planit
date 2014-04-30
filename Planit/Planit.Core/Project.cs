using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

        [Display(Name = "Parent Title")]
            public string ParentTitle { get; set; } // commenting out until we need it
            [Required]
            public string Title { get; set; }
            public string Note { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = false)]
            [Display(Name = "Due Date")]
            public DateTime DueDate { get; set; }

            [DataType(DataType.Date)]
        
            [Display(Name = "Start Date")]
            public DateTime StartDate { get; set; }
             [Range(0,100)]
            public int Status { get; set; }

            public String ChildrenStr { get; set; }

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
                if (ChildrenStr.CompareTo("")==0)
                {
                    ChildrenStr = child.ID.ToString();
                }
                else
                {
                    ChildrenStr = ChildrenStr + "," + child.ID;
                }
                child.UserID = this.UserID;
                child.ParentID = this.ID;
                child.ParentTitle = this.Title;
                child.Depth = this.Depth + 1;
                return child;
            }

            public String removeChildRef(Project child, Project parent)
            {
                StringBuilder childIDList = new StringBuilder("", 100);
                                               
                string[] ChildrenStrArray = parent.ChildrenStr.Split(',');

                foreach (string childStr in ChildrenStrArray)
                {
                    if (childStr.CompareTo(child.ID.ToString()) == 0 || (childStr.CompareTo("") == 0))
                    {
                        //throw away reference
                    }
                    else
                    {
                        childIDList.Append(childStr);
                        childIDList.Append(",");
                    }
                }
                parent.ChildrenStr = childIDList.ToString();
                return childIDList.ToString();
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
        public ProjectDbContext(string connectionString) : base("name=" + connectionString)
        { 
        }

        public DbSet<Project> Projects { get; set; }
    }

     
}