using System;
using System.Collections.Generic;

namespace Shapees.Models.TestModels
{
    public partial class Address
    {
        public Address()
        {
            Userprofile = new HashSet<Userprofile>();
        }

        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
        public string State { get; set; }

        public virtual ICollection<Userprofile> Userprofile { get; set; }
    }
}
