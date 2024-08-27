using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Has to be a maximum of fifty characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Has to be a maximum of a hundred characters")]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyID { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
