using System;
using System.Collections.Generic;

namespace Shapees.Models.TestModels
{
    public partial class Userprofile
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public int? AddressId { get; set; }
        public int Userid { get; set; }
        public byte[] Profileimage { get; set; }

        public virtual Address Address { get; set; }
        public virtual Users User { get; set; }
    }
}
