using Microsoft.EntityFrameworkCore;
using RESTApiCore.Models.Entities;

namespace RESTApiCore.Data
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext(DbContextOptions options) : base(options)
        {
        
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
