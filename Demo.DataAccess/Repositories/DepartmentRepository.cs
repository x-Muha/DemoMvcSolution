using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentModel;
using Microsoft.EntityFrameworkCore.Design.Internal;

namespace Demo.DataAccess.Repositories
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        // CRUD Opertatons
        // Get All
        public IEnumerable<Department> GetAll(bool WithTracking = false)//Default
        {
            if (WithTracking)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }
        // Get By Id                            "using Fat Arrow to return"
        public Department? GetById(int id) => _dbContext.Departments.Find(id);
        public int Update(Department department)// Update
        {
            _dbContext.Departments.Update(department);// Update Locally
            return _dbContext.SaveChanges(); // Update on Database
        }
        public int Remove(Department department)// Delete
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }
        public int Add(Department department)// Insert 
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }
    }
}
