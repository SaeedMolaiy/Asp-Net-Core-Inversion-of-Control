using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(new List<Person>
            {
                new Person
                {
                    PersonId = 1,
                    FullName = "Saeed Molaiy",
                    Age = 20
                }
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
