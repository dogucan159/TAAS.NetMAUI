using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure {
    internal class AuditAssignmentOperationAuditTypeConfiguration : IEntityTypeConfiguration<AuditAssignmentOperationAuditType> {
        public void Configure( EntityTypeBuilder<AuditAssignmentOperationAuditType> builder ) {
            builder.HasOne( x => x.AuditAssignment )
                  .WithMany( a => a.AuditAssignmentOperationAuditTypes )
                  .HasForeignKey( x => x.AuditAssignmentId )
                  .OnDelete( DeleteBehavior.Restrict )
                  .IsRequired();

            builder.HasOne( x => x.AuditType )
                  .WithMany( a => a.AuditAssignmentOperationAuditTypes )
                  .HasForeignKey( x => x.AuditTypeId )
                  .OnDelete( DeleteBehavior.Restrict )
                  .IsRequired();
        }
    }
}
