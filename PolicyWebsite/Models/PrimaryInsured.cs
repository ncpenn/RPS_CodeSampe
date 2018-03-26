using System.ComponentModel.DataAnnotations;

namespace Website.PolicyWebsite.Models
{
    public class PrimaryInsured
    {
        [Required]
        public string PrimaryInsuredFirstName { get; set; }

        public string PrimaryInsuredMiddleName { get; set; }

        [Required]
        public string PrimaryInsuredLastName { get; set; }

        [Required]
        public Address PrimaryInsuredAddress { get; set; }
    }
}