using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Configurations {
    public class ChecklistTaasFileConfiguration : IEntityTypeConfiguration<ChecklistTaasFile> {
        public void Configure( EntityTypeBuilder<ChecklistTaasFile> builder ) {
            builder.HasOne( b => b.Checklist )
                 .WithMany( b => b.ChecklistTaasFiles )
                 .HasForeignKey( x => x.ChecklistId )
                 .OnDelete( DeleteBehavior.Cascade )
                 .IsRequired();

            builder.HasOne( b => b.TaasFile )
                 .WithMany( b => b.ChecklistTaasFiles )
                 .HasForeignKey( x => x.TaasFileId )
                 .OnDelete( DeleteBehavior.Restrict )
                 .IsRequired();

        }
    }
}
