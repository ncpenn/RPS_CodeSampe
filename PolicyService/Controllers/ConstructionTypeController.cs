using Service.PolicyService.Models;
using Service.PolicyService.Repositories;
using Service.PolicyService.Security;
using System.Threading.Tasks;
using System.Web.Http;

namespace Service.PolicyService.Controllers
{
    [Authenticate]
    [RoutePrefix("api/constructiontype")]
    public class ConstructionTypeController : ApiController
    {
        public ConstructionTypeController(IRepository<ConstructionType> repository)
        {
            this.Repository = repository;
        }

        private IRepository<ConstructionType> Repository { get; }

        [HttpGet]
        [Route("fetchall")]
        public async Task<IHttpActionResult> FetchAll()
        {
            var constructionTypes = await this.Repository.FetchAll();

            return base.Ok(constructionTypes);
        }
    }
}
