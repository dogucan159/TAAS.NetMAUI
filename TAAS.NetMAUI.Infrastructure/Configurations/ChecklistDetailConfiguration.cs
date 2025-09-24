using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Configurations {
    public class ChecklistDetailConfiguration : IEntityTypeConfiguration<ChecklistDetail> {
        public void Configure( EntityTypeBuilder<ChecklistDetail> builder ) {

            builder.HasOne( b => b.Checklist )
                .WithMany( x => x.ChecklistDetails )
                .HasForeignKey( b => b.ChecklistId )
                .OnDelete( DeleteBehavior.Cascade )
                .IsRequired();

            builder.HasOne( b => b.ChecklistTemplateDetail )
                .WithMany()
                .HasForeignKey( b => b.ChecklistTemplateDetailId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();
        }
    }
}
