using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
         
        public EmployeeRepository(MVCDbContext dbcontext) : base(dbcontext)
        {
                
        }

        public IQueryable<Employee> SearchByName(string name)
        
           => _dbcontext.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower()));
        
    }
}
