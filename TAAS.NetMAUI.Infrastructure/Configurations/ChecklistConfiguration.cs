using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Configurations {
    public class ChecklistConfiguration : IEntityTypeConfiguration<Checklist> {
        public void Configure( EntityTypeBuilder<Checklist> builder ) {

            builder.HasOne( b => b.AuditAssignment )
                .WithMany()
                .HasForeignKey( b => b.AuditAssignmentId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();

            builder.HasOne( b => b.AuditType )
                .WithMany()
                .HasForeignKey( b => b.AuditTypeId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();

            builder.HasOne( b => b.AuditProgram )
                .WithMany()
                .HasForeignKey( b => b.AuditProgramId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired( false );

            builder.HasOne( b => b.ChecklistTemplate )
                .WithMany()
                .HasForeignKey( b => b.ChecklistTemplateId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();

            builder.HasOne( b => b.ReviewedAuditor )
                 .WithMany()
                 .HasForeignKey( b => b.ReviewedAuditorId )
                 .OnDelete( DeleteBehavior.Restrict )
                 .IsRequired( false );

        }
    }
}
