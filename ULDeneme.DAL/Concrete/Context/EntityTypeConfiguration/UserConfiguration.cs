using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Model.Entities;

namespace ULDeneme.DAL.Concrete.Context.EntityTypeConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).UseIdentityColumn();

            builder.Property(a => a.Email).HasMaxLength(100).IsRequired();

            builder.Property(a => a.Password).HasMaxLength(20).IsRequired();
        }
    }
}
