using Website.PolicyWebsite.Client;
using Website.PolicyWebsite.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Website.PolicyWebsite.Controllers
{
    public class InsurancePolicyController : Controller
    {
        public InsurancePolicyController(IPolicyServiceClient httpClient)
        {
            this.PolicyServiceClient = httpClient;
        }

        private IPolicyServiceClient PolicyServiceClient { get; }

        public async Task<ActionResult> Index()
        {
            var policies = await this.PolicyServiceClient.FetchAll();
            return View(policies);
        }

        public async Task<PartialViewResult> Add()
        {
            var constructionTypes = await this.PolicyServiceClient.GetConstructionTypes();
            return PartialView("Partial/_AddPolicy", new AddPolicy(constructionTypes));
        }

        [HttpPost]
        public async Task<string> Add(AddPolicy addPolicy)
        {
            return await this.PolicyServiceClient.Save(addPolicy);
        }
    }
}