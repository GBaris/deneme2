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
    internal class VocabularyConfiguration : IEntityTypeConfiguration<Vocabulary>
    {
        public void Configure(EntityTypeBuilder<Vocabulary> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.ID).UseIdentityColumn();

            builder.HasOne(t => t.Sozluk).WithMany(t => t.Vocabularies).HasForeignKey(t => t.SozlukID);

            builder.Property(t => t.UnKVoc).IsRequired();
            builder.Property(t => t.KVoc).IsRequired();


        }
    }
}
