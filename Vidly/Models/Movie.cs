﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        public Genre Genre { get; set; }
        [Required]
        [Display(Name ="Genre")]
        public short? GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [Display(Name ="Date Added")]
        public DateTime DateAdded { get; set; }
        [Required]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }
    }
}
