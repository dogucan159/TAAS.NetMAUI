using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure {
    public class AuditAssignmentAuditorConfiguration : IEntityTypeConfiguration<AuditAssignmentAuditor> {
        public void Configure( EntityTypeBuilder<AuditAssignmentAuditor> builder ) {
            builder.HasOne( x => x.AuditAssignment )
                  .WithMany( a => a.AuditAssignmentAuditors )
                  .HasForeignKey( x => x.AuditAssignmentId )
                  .OnDelete( DeleteBehavior.Restrict )
                  .IsRequired();

            builder.HasOne( x => x.Auditor )
                  .WithMany( a => a.AuditAssignmentAuditors )
                  .HasForeignKey( x => x.AuditorId )
                  .OnDelete( DeleteBehavior.Restrict )
                  .IsRequired();
        }
    }
}
