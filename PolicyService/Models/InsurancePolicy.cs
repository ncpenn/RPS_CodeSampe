using System;
using System.ComponentModel.DataAnnotations;

namespace Service.PolicyService.Models
{
    public class InsurancePolicy
    {
        [Required]
        public string PolicyNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime Expiration { get; set; }

        [Required]
        public PrimaryInsured PrimaryInsured { get; set; }

        [Required]
        public InsuredRisk InsuredRisk { get; set; }
    }
}