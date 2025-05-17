using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: BaseEntity
    {
        int Add(TEntity entity);
        IEnumerable<TEntity> GetAll(bool WithTracking = false);
        TEntity? GetById(int id);
        int Remove(TEntity entity);
        int Update(TEntity entity);
    }
}
