using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : 
                          IGenericRepository<TEntity> where TEntity : BaseEntity{
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)//Default
        {
            if (WithTracking)
                return _dbContext.Set<TEntity>().Where(E=>E.IsDeleted != true).ToList();
            else
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();}
        // Get By Id                            "using Fat Arrow to return"
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);
        public int Update(TEntity entity)// Update
        {
            _dbContext.Set<TEntity>().Update(entity);// Update Locally
            return _dbContext.SaveChanges();} // Update on Database
        public int Remove(TEntity entity)// Delete
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();}
        public int Add(TEntity entity)// Insert 
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();}

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true)
                             .Select(selector).ToList();// to imediate execute
        }
    }
}
