using Demo.DataAccess.Models.DepartmentModel;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {// Used when specific methods needed (not generic)
    }
}