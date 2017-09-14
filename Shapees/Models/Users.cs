﻿/* ---------------------------------- SHAPEES 2017 ---------------------------------- */
/*                                                                                    */
/* Created and Edited by Four Corner Solutions team:                                  */
/* Ivana Ozakovic, Nicole Lardner, Damon Walker,                                      */
/* Cassandra Kalabric, Matthew Mauri, Sima Narain                                     */
/*                                                                                    */
/* Created for Kid's Uni, University of Wollongong, Wollongong                        */
/*                                                                                    */
/* Filename: Models/Users.cs                                                          */
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
    public partial class Users
    {
        private ShapeesDB context;

        public int UserID { get; set; }
        public string Username {get; set;}
        public string Email { get; set; }
        public string Pass { get; set; }
        public int user_type { get; set; }
        public DateTime last_login { get; set; }
        public int is_logged_in { get; set; }
    }
}
