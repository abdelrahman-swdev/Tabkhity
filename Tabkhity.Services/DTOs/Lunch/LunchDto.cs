using System.ComponentModel.DataAnnotations;

namespace Tabkhity.Services.DTOs.Lunch
{
    public class LunchToSaveDto
    {
        [Required]
        public string Name { get; set; }
    }
}
