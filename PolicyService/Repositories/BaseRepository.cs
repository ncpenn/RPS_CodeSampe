using Service.PolicyService.DAL;
using Service.PolicyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.PolicyService.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        public BaseRepository(PolicyDatabaseEntities contextProvider)
        {
            this.Context = contextProvider;
        }

        protected PolicyDatabaseEntities Context;

        public abstract Task<IEnumerable<T>> FetchAll();

        public abstract Task<Result> Add(T TObject);

        public void Dispose()
        {
            // Autofac manages the dispose calls via its lifetype scope management
            this.Context.Dispose();
        }
    }
}