using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(IDepartmentRepository departmentRepository,
                          IEmployeeRepository employeeRepository,
                          ApplicationDbContext dbContext)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _dbContext = dbContext;
        }
        public IEmployeeRepository employeeRepository => _employeeRepository;
        public IDepartmentRepository departmentRepository => _departmentRepository;
        public int SaveChanges() => _dbContext.SaveChanges();
    }
}


