﻿using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Entities
{
    public class AvailabilityStatus
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
