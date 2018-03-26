using Service.PolicyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.PolicyService.Repositories.Interfaces
{
    public interface IPolicyRepository
    {
        Task<bool> Add(InsurancePolicy insurancePolicy);

        Task<IEnumerable<InsurancePolicy>> GetPolicies(int pageNumber, int pageSize);
    }
}