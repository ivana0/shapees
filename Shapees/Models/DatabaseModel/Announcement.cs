/* ---------------------------------- SHAPEES 2017 ---------------------------------- */
/*                                                                                    */
/* Created and Edited by Four Corner Solutions team:                                  */
/* Ivana Ozakovic, Nicole Lardner, Damon Walker,                                      */
/* Cassandra Kalabric, Matthew Mauri, Sima Narain                                     */
/*                                                                                    */
/* Created for Kid's Uni, University of Wollongong, Wollongong                        */
/*                                                                                    */
/* Filename: Models/DatabaseModels/Announcement.cs                                    */
/*                                                                                    */
/* File description: DB Context - ShapeesDB for creating other models.                */
/* ---------------------------------------------------------------------------------- */

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
