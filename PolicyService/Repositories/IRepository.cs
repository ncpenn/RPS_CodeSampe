using Service.PolicyService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.PolicyService.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        Task<IEnumerable<T>> FetchAll();

        Task<Result> Add(T TObject);
    }
}