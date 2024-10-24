using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DbzProject.Service
{ 
    public class DataAccessService:DbContext
    {
        #region TABLES
        // DbSet pour les entités
        public DbSet<DbzCharacter> Characters { get; set;}
        public DbSet<User> Users { get; set; }
        #endregion


        #region CONSTRUCTOR    
        public DataAccessService(DbContextOptions<DataAccessService> options):base(options)
        {
            // Vérifie si la base de données existe avant de la créer

            try
            {
                // Vérifie si la base de données existe avant de la créer
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbzCharacter>()
                .HasKey(x => x.IdCharacter);

            modelBuilder.Entity<User>()
                .HasKey(x => x.IdUser);

            modelBuilder.Entity<DbzCharacter>()
                .HasOne(c => c.UserCreator) // Chaque DbzCharacter appartient à un seul User
                .WithMany(u => u.Characters) // Un User peut avoir plusieurs DbzCharacters
                .HasForeignKey(c => c.UserId) // Clé étrangère dans DbzCharacter
                .HasPrincipalKey(e => e.IdUser); // Clé principale dans User

        }

    }
}
