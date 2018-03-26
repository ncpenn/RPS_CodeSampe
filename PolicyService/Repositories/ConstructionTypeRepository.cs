using Service.PolicyService.DAL;
using Service.PolicyService.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Service.PolicyService.Repositories
{
    public class ConstructionTypeRepository : BaseRepository<Models.ConstructionType>
    {
        public ConstructionTypeRepository(PolicyDatabaseEntities context) 
            : base(context)
        {
        }

        public override Task<Result> Add(Models.ConstructionType TObject)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<IEnumerable<Models.ConstructionType>> FetchAll()
        {
            var constructionTypes = await base.Context.ConstructionTypes.Select(
                c => new Models.ConstructionType
                {
                    Id = c.ConstructionTypeId.ToString(),
                    Name = c.Name
                }).ToListAsync();

            return constructionTypes;
        }
    }
}