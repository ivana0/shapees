using System;
using System.Collections.Generic;

namespace Shapees.Models.DatabaseModel
{
    public partial class Task
    {
        public int Taskid { get; set; }
        public string Taskforeducator { get; set; }
        public string Taskforchild { get; set; }
        public int Assignedforid { get; set; }
        public int Taskforfirst { get; set; }
        public int Taskforlast { get; set; }
        public string Authorfirst { get; set; }
        public string Authorlast { get; set; }
        public int Authorid { get; set; }
        public int Childid { get; set; }
        public string Childfirst { get; set; }
        public string Childlast { get; set; }
        public DateTime Dateassigned { get; set; }
        public DateTime Datecompleted { get; set; }
        public DateTime Duedate { get; set; }
        public int Reportid { get; set; }
        public string Reportpath { get; set; }
        public int Issubmitted { get; set; }
        public int Iscompleted { get; set; }

        public virtual Userinfo Assignedfor { get; set; }
        public virtual Childinfo Child { get; set; } 
        public virtual Report Report { get; set; }
    }
}
