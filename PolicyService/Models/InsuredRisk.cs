using System.ComponentModel.DataAnnotations;

namespace Service.PolicyService.Models
{
    public class InsuredRisk
    {
        [Required]
        public ConstructionType ConstructionType { get; set; }

        [Required]
        public string YearBuilt { get; set; }

        [Required]
        public Address Location { get; set; }
    }
}