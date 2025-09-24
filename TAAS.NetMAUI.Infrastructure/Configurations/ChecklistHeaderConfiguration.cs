using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Configurations {
    public class ChecklistHeaderConfiguration : IEntityTypeConfiguration<ChecklistHeader> {
        public void Configure( EntityTypeBuilder<ChecklistHeader> builder ) {

            builder.HasOne( b => b.Checklist )
                .WithMany( x => x.ChecklistHeaders )
                .HasForeignKey( b => b.ChecklistId )
                .OnDelete( DeleteBehavior.Cascade )
                .IsRequired();

            builder.HasOne( b => b.ChecklistTemplateHeader )
                .WithMany()
                .HasForeignKey( b => b.ChecklistTemplateHeaderId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();
        }
    }
}
