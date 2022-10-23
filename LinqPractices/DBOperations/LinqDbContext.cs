using LinqPractices.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LinqPractices.DBOperations
{
    public class LinqDbContext : DbContext
    {     
        public DbSet<Student> Students { get; set; }=null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("LinqDatabase");
        }

    }
}