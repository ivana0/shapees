using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int? Roomid { get; set; }
        public string Inroom { get; set; }
        public string Educatorfname { get; set; }
        public string Educatorlname { get; set; }
        //created educator's full name variable - not stored in DB
        public string EducatorFullName
        {
            get
            {
                return Educatorfname + " " + Educatorlname;
            }
        }
        public int Educatorid { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int? Postcode { get; set; }
        public string State { get; set; }
        public string Childfirstname { get; set; }
        public string Childlastname { get; set; }
        //created child's full name variable - not stored in DB
        public string FullName
        {
            get
            {
                return Childfirstname + " " + Childlastname;
            }
        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Dob { get; set; }
        //set automatically - caluclate based on DOB
        public string Currentage { get; set; }
        public string Contacnumber1 { get; set; }
        public string Contacnumber2 { get; set; }
        public int? Parent1 { get; set; }
        public string Parent1fname { get; set; }
        public string Parent1lname { get; set; }
        //created parent1's full name variable - not stored in DB
        public string Parent1FullName
        {
            get
            {
                return Parent1fname + " " + Parent1lname;
            }
        }
        public int? Parent2 { get; set; }
        public string Parent2fname { get; set; }
        public string Parent2lname { get; set; }
        //created parent2's full name variable - not stored in DB
        public string Parent2FullName
        {
            get
            {
                return Parent2fname + " " + Parent2lname;
            }
        }
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
        public virtual Room Room { get; set; }
    }
}
