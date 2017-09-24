using System;
using System.Collections.Generic;

namespace Shapees.Models.DatabaseModel
{
    public partial class Userinfo
    {
        public Userinfo()
        {
            ChildinfoEducator = new HashSet<Childinfo>();
            ChildinfoParent1Navigation = new HashSet<Childinfo>();
            ChildinfoParent2Navigation = new HashSet<Childinfo>();
            Document = new HashSet<Document>();
            Media = new HashSet<Media>();
            MessageAuthor = new HashSet<Message>();
            MessageReceiver = new HashSet<Message>();
            Report = new HashSet<Report>();
            Task = new HashSet<Task>();
        }

        public int Userid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int Usertype { get; set; }
        public string Usertypename { get; set; }
        public DateTime? Lastlogin { get; set; }
        public int Isloggedin { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int? Postcode { get; set; }
        public string State { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? Dob { get; set; }
        public string Homephone { get; set; }
        public string Mobilephone { get; set; }
        public DateTime? Employedon { get; set; }
        public string Roomassigned { get; set; }
        public int? Roomid { get; set; }
        public string Shortbio { get; set; }
        public int? Taskscompleted { get; set; }
        public int? Totaltasks { get; set; }
        public string Othercontact { get; set; }
        public string Parentof { get; set; }
        public string Profileimage { get; set; }

        public virtual ICollection<Childinfo> ChildinfoEducator { get; set; }
        public virtual ICollection<Childinfo> ChildinfoParent1Navigation { get; set; }
        public virtual ICollection<Childinfo> ChildinfoParent2Navigation { get; set; }
        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Message> MessageAuthor { get; set; }
        public virtual ICollection<Message> MessageReceiver { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual Room Room { get; set; }
    }
}
