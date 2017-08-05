using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shapees.Models
{
    public partial class Users
    {
        private ShapeesDB context;

        public int UserID { get; set; }
        public string Username {get; set;}
        public string Email { get; set; }
        public string pass { get; set; }
        public string user_type { get; set; }
    }
}
