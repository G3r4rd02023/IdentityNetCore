using IdentityNetCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityNetCore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Pelicula> Peliculas { get; set; }
    }
}