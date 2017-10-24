using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shapees.Models.DatabaseModel
{
    public partial class Media
    {
        public int Mediaid { get; set; }
        public string Mediatype { get; set; }
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Dateuploaded { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Filepath { get; set; }

        public virtual Userinfo Author { get; set; }
        public virtual Childinfo Child { get; set; }
    }
}
