using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.DAL.Concrete.Context.EntityTypeConfiguration;
using ULDeneme.Model.Entities;

namespace ULDeneme.DAL.Concrete.Context
{
    public class ULDenemeDbContext : DbContext
    {
        public ULDenemeDbContext(DbContextOptions<ULDenemeDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=OSMAN; Database=ULDenemeDB; Trusted_Connection=True; TrustServerCertificate=True");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TranslationType> Translations { get; set; }
        public DbSet<Sozluk> Sozluks { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TranslationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SozlukConfiguration());
            modelBuilder.ApplyConfiguration(new VocabularyConfiguration());
        }
    }
}
