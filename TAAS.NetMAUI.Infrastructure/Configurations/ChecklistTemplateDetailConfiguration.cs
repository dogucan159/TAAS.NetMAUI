using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Configurations {
    public class ChecklistTemplateDetailConfiguration : IEntityTypeConfiguration<ChecklistTemplateDetail> {
        public void Configure( EntityTypeBuilder<ChecklistTemplateDetail> builder ) {
            builder.HasOne( b => b.ChecklistTemplate )
                .WithMany()
                .HasForeignKey( b => b.ChecklistTemplateId )
                .OnDelete( DeleteBehavior.Cascade )
                .IsRequired();
        }
    }
}
