using System;
using System.Collections.Generic;

namespace Shapees.Models.DatabaseModel
{
    public partial class Childinfo
    {
        public Childinfo()
        {
            Document = new HashSet<Document>();
            Media = new HashSet<Media>();
            Report = new HashSet<Report>();
            Task = new HashSet<Task>();
        }

        public int Childid { get; set; }
        public string Inroom { get; set; }
        public string Educatorfname { get; set; }
        public string Educatorlname { get; set; }
        public int Educatorid { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int? Postcode { get; set; }
        public string State { get; set; }
        public string Childfirstname { get; set; }
        public string Childlastname { get; set; }
        public DateTime Dob { get; set; }
        public string Currentage { get; set; }
        public string Contacnumber1 { get; set; }
        public string Contacnumber2 { get; set; }
        public int? Parent1 { get; set; }
        public string Parent1fname { get; set; }
        public string Parent1lname { get; set; }
        public int? Parent2 { get; set; }
        public string Parent2fname { get; set; }
        public string Parent2lname { get; set; }
        public string Shortinfo { get; set; }
        public string Specialneeds { get; set; }
        public string Profileimage { get; set; }

        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual Userinfo Educator { get; set; }
        public virtual Userinfo Parent1Navigation { get; set; }
        public virtual Userinfo Parent2Navigation { get; set; }
    }
}
