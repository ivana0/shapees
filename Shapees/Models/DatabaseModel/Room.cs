using System;
using System.Collections.Generic;

namespace Shapees.Models.DatabaseModel
{
    public partial class Room
    {
        public Room()
        {
            Childinfo = new HashSet<Childinfo>();
            Userinfo = new HashSet<Userinfo>();
        }

        public int Roomid { get; set; }
        public string Roomname { get; set; }
        public string Roomagegroup { get; set; }
        public string Info { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Childinfo> Childinfo { get; set; }
        public virtual ICollection<Userinfo> Userinfo { get; set; }
    }
}
