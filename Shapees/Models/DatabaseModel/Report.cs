using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shapees.Models.DatabaseModel
{
    public partial class Report
    {
        public Report()
        {
            Task = new HashSet<Task>();
        }

        public int Reportid { get; set; }
        public string Reporttype { get; set; }
        public string Authorfirst { get; set; }
        public string Authorlast { get; set; }
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
        public DateTime Datecreated { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Lastmodified { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Bodytext { get; set; }
        public string Filepath { get; set; }
        public DateTime? Datesubmitted { get; set; }
        public DateTime? Datecompleted { get; set; }
        public int Issubmitted { get; set; }
        public int Iscompleted { get; set; }
        public int? Taskid { get; set; }
        public DateTime? Duedate { get; set; }
        public string Attachmentpaths { get; set; }
        public int? Attachmentcount { get; set; }

        public virtual ICollection<Task> Task { get; set; }
        public virtual Userinfo Author { get; set; }
        public virtual Childinfo Child { get; set; }
    }
}
