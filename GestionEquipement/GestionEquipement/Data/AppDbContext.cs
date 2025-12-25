using GestionEquipement.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEquipement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Piece> piece{ get; set; }
        public DbSet<equipementModel> equipement { get; set; }
    }
} 