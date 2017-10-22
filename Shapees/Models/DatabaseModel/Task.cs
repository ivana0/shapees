using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        //created author's full name variable - not stored in DB
        public string AuthorFullName
        {
            get
            {
                return Authorfirst + " " + Authorlast;
            }
        }
    
        public int Authorid { get; set; }
        public int Childid { get; set; }
        public string Childfirst { get; set; }
        public string Childlast { get; set; }
        //created child's full name variable - not stored in DB
        public string ChildFullName
        {
            get
            {
                return Childfirst + " " + Childlast;
            }
        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Dateassigned { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Datecompleted { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
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
