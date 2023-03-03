using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Model.Entities;

namespace ULDeneme.DAL.Concrete.Context.EntityTypeConfiguration
{
    internal class TranslationTypeConfiguration : IEntityTypeConfiguration<TranslationType>
    {
        public void Configure(EntityTypeBuilder<TranslationType> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.ID).UseIdentityColumn();

            builder.HasOne(t => t.User)
                .WithMany(u => u.TranslationTypes)
                .HasForeignKey(t => t.UserID);

            builder.Property(t => t.UserRole).IsRequired();
            builder.Property(t => t.KnownLang).HasMaxLength(50).IsRequired();
            builder.Property(t => t.UnknownLang).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Name).HasMaxLength(150).IsRequired();
        }
    }
}
