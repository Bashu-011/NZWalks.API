﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class PostNewRegion
    {
        [Required]
        [MaxLength(5, ErrorMessage = "Has to be a maximum of five characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Max length has to be 50 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
