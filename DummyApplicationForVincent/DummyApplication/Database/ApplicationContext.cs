using DummyApplication.Models;
using System.Data.Entity;

namespace DummyApplication.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("sqlServerConnectionString")
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Resturants> Resturants { get; set; }
    }
}