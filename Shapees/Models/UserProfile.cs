/* ---------------------------------- SHAPEES 2017 ---------------------------------- */
/*                                                                                    */
/* Created and Edited by Four Corner Solutions team:                                  */
/* Ivana Ozakovic, Nicole Lardner, Damon Walker,                                      */
/* Cassandra Kalabric, Matthew Mauri, Sima Narain                                     */
/*                                                                                    */
/* Created for Kid's Uni, University of Wollongong, Wollongong                        */
/*                                                                                    */
/* Filename: Models/UserProfile.cs                                                    */
/* DB Context file: Models/ShapeesDB.cs                                               */
/*                                                                                    */
/* File description: Users entity type model.                                         */
/* ---------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shapees.Models
{
    public partial class UserProfile
    {
        private ShapeesDB context;

        public int profile_id { get; set; }
        public string FirstName {get; set;}
        public string LastName { get; set; }
        public DateTime UserDOB { get; set; }
        public string HomePhone { get; set; }
        [DisplayFormat(NullDisplayText = "No Mobile")]
        public string MobilePhone { get; set; }

        public byte[] ProfileImage { get; set; }

       // public int? UserAddressID { get; set; }
       // public Address Address { get; set; }

}
}
