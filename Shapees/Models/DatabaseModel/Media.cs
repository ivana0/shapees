using System;
using System.Collections.Generic;

namespace Shapees.Models.DatabaseModel
{
    public partial class Media
    {
        public int Mediaid { get; set; }
        public string Mediatype { get; set; }
        public string Authorfirst { get; set; }
        public string Authorlast { get; set; }
        public int Authorid { get; set; }
        public int Childid { get; set; }
        public string Childfirst { get; set; }
        public string Childlast { get; set; }
        public DateTime Dateuploaded { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Filepath { get; set; }

        public virtual Userinfo Author { get; set; }
        public virtual Childinfo Child { get; set; }
    }
}
