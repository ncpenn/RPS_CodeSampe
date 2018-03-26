using System.ComponentModel.DataAnnotations;

namespace Website.PolicyWebsite.Models
{
    public class InsuredRisk
    {
        [Required]
        public BuildingType ConstructionType { get; set; }

        [Required]
        public string YearBuilt { get; set; }

        [Required]
        public Address Location { get; set; }
    }
}