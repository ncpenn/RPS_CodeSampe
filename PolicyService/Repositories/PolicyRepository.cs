using AutoMapper;
using Service.PolicyService.DAL;
using Service.PolicyService.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Service.PolicyService.Repositories
{
    public class PolicyRepository : BaseRepository<InsurancePolicy>
    {
        public PolicyRepository(PolicyDatabaseEntities context)
            : base(context)
        {
        }

        public override async Task<IEnumerable<InsurancePolicy>> FetchAll()
        {
            var insurancePolicies = await (from p in base.Context.Policies
                                           join pi in base.Context.PrimaryInsureds on p.PolicyNumber equals pi.PolicyNumber
                                           select new InsurancePolicy
                                           {
                                               EffectiveDate = p.EffectiveDate,
                                               Expiration = p.ExpirationDate,
                                               PolicyNumber = p.PolicyNumber.ToString(),
                                               PrimaryInsured = new Models.PrimaryInsured
                                               {
                                                   PrimaryInsuredFirstName = pi.FirstName,
                                                   PrimaryInsuredLastName = pi.LastName
                                               }
                                           }).ToListAsync();

            return insurancePolicies;
        }

        public override async Task<Result> Add(InsurancePolicy insurancePolicy)
        {
            var result = new Result();
            try
            {
                var policy = Mapper.Map<Policy>(insurancePolicy);
                base.Context.Policies.Add(policy);
                await base.Context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                result.ErrorMessage = $"{e.Message} {e.InnerException?.Message}";
            }
            catch (DataException e)
            {
                result.ErrorMessage = e.Message;
            }

            return result;
        }
    }
}