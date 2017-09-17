/* ---------------------------------- SHAPEES 2017 ---------------------------------- */
/*                                                                                    */
/* Created and Edited by Four Corner Solutions team:                                  */
/* Ivana Ozakovic, Nicole Lardner, Damon Walker,                                      */
/* Cassandra Kalabric, Matthew Mauri, Sima Narain                                     */
/*                                                                                    */
/* Created for Kid's Uni, University of Wollongong, Wollongong                        */
/*                                                                                    */
/* Filename: Models/Address.cs                                                        */
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
    public partial class Address
    {
        private ShapeesDB context;

        public int AddressID { get; set; }
        public string Street {get; set;}
        public string City { get; set; }
        public string State { get; set; }
        public int Postcode { get; set; }


    }
}
