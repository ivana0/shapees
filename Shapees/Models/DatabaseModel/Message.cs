using System;
using System.Collections.Generic;

namespace Shapees.Models.DatabaseModel
{
    public partial class Message
    {
        public int Messageid { get; set; }
        public string Authorfirst { get; set; }
        public string Authorlast { get; set; }
        public int Authorid { get; set; }
        public string Receiverfirst { get; set; }
        public string Receiverlast { get; set; }
        public int Receiverid { get; set; }
        public DateTime Datesent { get; set; }
        public DateTime? Datereceived { get; set; }
        public DateTime? Dateopened { get; set; }
        public int Isopened { get; set; }
        public int? Isrepliedto { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public virtual Userinfo Author { get; set; }
        public virtual Userinfo Receiver { get; set; }
    }
}
