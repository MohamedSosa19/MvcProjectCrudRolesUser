using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Context
{
    public class MVCDbContext: IdentityDbContext<ApplicationUser>
    {

        public MVCDbContext(DbContextOptions<MVCDbContext> options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder.UseSqlServer("Server =DESKTOP-KQ35VG5\\SQLEXPRESS;Database=MVCDbContext;Trusted_Connection=true;MultipleActiveResultSets=true");


        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
