using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Website.PolicyWebsite.Models
{
    public class AddPolicy
    {
        public AddPolicy()
        {
        }

        public AddPolicy(IEnumerable<BuildingType> constructionTypes)
        {
            this.RiskConstruction = constructionTypes.Select(
                c => new SelectListItem { Text = c.Name, Value = c.Id });
        }

        [Display(Name = "Policy Number")]
        public string PolicyNumber { get; set; }

        [Display(Name = "Effective Date")]
        public string EffectiveDate { get; set; }

        [Display(Name = "Expiration Date")]
        public string ExpireDate { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Street Address")]
        public string Street { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "City")]
        public string State { get; set; }

        [Display(Name = "Zip")]
        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        public string SelectedRiskConstruction { get; set; }

        [Display(Name = "Risk Construction")]
        public IEnumerable<SelectListItem> RiskConstruction { get; set; }

        [Display(Name = "Year Built")]
        public string YearBuilt { get; set; }

        [Display(Name = "Street Address")]
        public string BuildingStreet { get; set; }

        [Display(Name = "City")]
        public string BuildingCity { get; set; }

        [Display(Name = "State")]
        public string BuildingState { get; set; }

        [Display(Name = "Zip")]
        [DataType(DataType.PostalCode)]
        public string BuildingZip { get; set; }
    }
}