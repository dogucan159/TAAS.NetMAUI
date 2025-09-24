using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Configurations {
    public class ChecklistDetailTaasFileConfiguration : IEntityTypeConfiguration<ChecklistDetailTaasFile> {
        public void Configure( EntityTypeBuilder<ChecklistDetailTaasFile> builder ) {
            builder.HasOne( b => b.ChecklistDetail )
                 .WithMany( b => b.ChecklistDetailTaasFiles )
                 .HasForeignKey( x => x.ChecklistDetailId )
                 .OnDelete( DeleteBehavior.Cascade )
                 .IsRequired();

            builder.HasOne( b => b.TaasFile )
                 .WithMany( b => b.ChecklistDetailTaasFiles )
                 .HasForeignKey( x => x.TaasFileId )
                 .OnDelete( DeleteBehavior.Restrict )
                 .IsRequired();

        }
    }
}
