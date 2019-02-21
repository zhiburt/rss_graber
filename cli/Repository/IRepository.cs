using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cli.Repository
{
    interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(string id);
        Task<bool> Create(TEntity entity);
        Task Update(string id, TEntity entity);
        Task Delete(string id);
    }
}
