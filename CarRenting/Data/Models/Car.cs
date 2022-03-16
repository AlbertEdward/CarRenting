﻿using System.ComponentModel.DataAnnotations;
using static CarRenting.Data.DataConstants;

namespace CarRenting.Data.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CarMakeMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(CarModelMinLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(CarDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
