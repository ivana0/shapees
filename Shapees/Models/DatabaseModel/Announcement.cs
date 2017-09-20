using System;
using System.Collections.Generic;

namespace Shapees.Models.DatabaseModel
{
    public partial class Announcement
    {
        public int Announcementid { get; set; }
        public string Announcementtype { get; set; }
        public DateTime Datecreated { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Isdisplayed { get; set; }
        public string Announcementfor { get; set; }
    }
}
