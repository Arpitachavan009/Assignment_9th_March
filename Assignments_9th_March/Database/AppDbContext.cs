using Assignments_9th_March.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignments_9th_March.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Employee> tbl_Employees { get; set; }
        
    }
}
