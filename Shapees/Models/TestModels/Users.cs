using System;
using System.Collections.Generic;

namespace Shapees.Models.TestModels
{
    public partial class Users
    {
        public Users()
        {
            Userprofile = new HashSet<Userprofile>();
        }

        public int Userid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int UserType { get; set; }
        public DateTime? LastLogin { get; set; }
        public int IsLoggedIn { get; set; }
        public int? ProfileId { get; set; }

        public virtual ICollection<Userprofile> Userprofile { get; set; }
    }
}
