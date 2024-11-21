using Microsoft.EntityFrameworkCore;
using MonApplicationMVC.Models;

namespace MonApplicationMVC.Data
{
    public class AppDbContext : DbContext
    {
        // Constructeur du DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet pour User (table Users)
        public DbSet<User_log> User_log { get; set; }
        public DbSet<Entree> Entree { get; set; }
        public DbSet<Lot> Lots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_log>()
                .ToTable("User_log")  // Correct table name for User_log
                .HasKey(u => u.idUser_log);  // Primary key for User_log

            modelBuilder.Entity<Entree>()
                .ToTable("Entree")  // Table name for Entree
                .HasKey(e => e.NumEntree);  // Primary key for Entree

            modelBuilder.Entity<Lot>()
                .ToTable("Lots")  // Nom correct de la table Lot
                .HasKey(l => l.NumLot);  // Clé primaire pour Lot
        }


    }
}
