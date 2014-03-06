using System;
using System.Collections.Generic;
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
        
            public string ParentID { get; set; }

            public string Description { get; set; }

            public DateTime DueDate { get; set; }

            public DateTime StartDate { get; set; }
        
        #endregion 

        #region Constructors

            public Project()
            {

            }

            public Project(int ID, string UserID, int Depth, string ParentID, string Description, DateTime  DueDate, DateTime StartDate)
            {
                this.ID = ID;
                this.UserID = UserID;
                this.Depth = Depth;
                this.ParentID = ParentID;
                this.Description = Description;
                this.DueDate = DueDate;
                this.StartDate = StartDate;
            }
        #endregion 

    }
}