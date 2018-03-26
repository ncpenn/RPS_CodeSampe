using Website.PolicyWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.PolicyWebsite.Client
{
    public interface IPolicyServiceClient
    {
        Task<IEnumerable<DisplayPolicy>> FetchAll();

        Task<string> Save(AddPolicy addPolicy);

        Task<IEnumerable<BuildingType>> GetConstructionTypes();
    }
}
