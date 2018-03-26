using Service.PolicyService.Security;
using Service.PolicyService.Models;
using System.Web.Http;
using System.Threading.Tasks;
using Service.PolicyService.Repositories;

namespace Service.PolicyService.Controllers
{
    [Authenticate]
    [RoutePrefix("api/policy")]
    public class PolicyController : ApiController
    {
        public PolicyController(IRepository<InsurancePolicy> policyRepository)
        {
            this.PolicyRepo = policyRepository;
        }

        private IRepository<InsurancePolicy> PolicyRepo { get; }

        [HttpGet]
        [Route("fetchall")]
        public async Task<IHttpActionResult> GetPolicies()
        {
            var policies = await this.PolicyRepo.FetchAll();

            return base.Ok(policies);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add(InsurancePolicy policy)
        {
            if (policy == null || !ModelState.IsValid)
            {
                return base.BadRequest();
            }

            var result = await this.PolicyRepo.Add(policy);

            return base.Ok(result);
        }
    }
}
