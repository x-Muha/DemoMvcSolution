using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.DataAccess.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext dbContext) : 
        GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
    }
}
