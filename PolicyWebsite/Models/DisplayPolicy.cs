using System;
using System.ComponentModel.DataAnnotations;

namespace Website.PolicyWebsite.Models
{
    public class DisplayPolicy
    {
        [Display(Name = "Policy Number")]
        public int PolicyNumber { get; set; }

        [Display(Name = "Insured Name")]
        public string InsuredName {
            get
            {
                return $"{this.PrimaryInsuredFirstName} {this.PrimaryInsuredLastName}";
            }
        }

        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime EffectiveDate { get; set; }

        [Display(Name = "Expiration Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Expiration { get; set; }

        public string PrimaryInsuredFirstName { get; set; }

        public string PrimaryInsuredLastName { get; set; }
    }
}