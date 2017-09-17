﻿/* ---------------------------------- SHAPEES 2017 ---------------------------------- */
/*                                                                                    */
/* Created and Edited by Four Corner Solutions team:                                  */
/* Ivana Ozakovic, Nicole Lardner, Damon Walker,                                      */
/* Cassandra Kalabric, Matthew Mauri, Sima Narain                                     */
/*                                                                                    */
/* Created for Kid's Uni, University of Wollongong, Wollongong                        */
/*                                                                                    */
/* Filename: Models/UserType/Educator.cs                                              */
/* DB Context file: Models/ShapeesDB.cs                                               */
/*                                                                                    */
/* File description: Educator user type model.                                        */
/* ---------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shapees.Models
{
    public partial class Educator
    {
        private ShapeesDB context;

        public int EducatorID { get; set; }
        public DateTime EmployedOn { get; set; }
        public string ShortBio { get; set; }
    }
}
