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
    internal class SozlukConfiguration : IEntityTypeConfiguration<Sozluk>
    {
        public void Configure(EntityTypeBuilder<Sozluk> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.ID).UseIdentityColumn();

            builder.HasOne(t => t.User)
                .WithMany(u => u.Sozluks)
                .HasForeignKey(t => t.UserID);

            builder.HasOne(t => t.TranslationType).WithMany(u => u.Sozluks).HasForeignKey(t => t.TranslationTypeID);

            builder.Property(t=>t.Theme).IsRequired();
            builder.Property(T=>T.Description).HasMaxLength(255);
            builder.Property(t => t.UserRole).IsRequired();
            builder.Property(t => t.Name).HasMaxLength(64).IsRequired();

        }
    }
}
