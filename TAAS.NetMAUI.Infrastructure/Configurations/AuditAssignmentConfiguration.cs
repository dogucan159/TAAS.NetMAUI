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

namespace TAAS.NetMAUI.Infrastructure.Configurations {
    public class AuditAssignmentConfiguration : IEntityTypeConfiguration<AuditAssignment> {
        public void Configure( EntityTypeBuilder<AuditAssignment> builder ) {

            builder.HasOne( a => a.CoordinatorAuditor )
                .WithMany()
                .HasForeignKey( a => a.CoordinatorAuditorId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();

            builder.HasOne( a => a.StrategyPlanPeriod )
                    .WithMany()
                    .HasForeignKey( a => a.StrategyPlanPeriodId )
                    .OnDelete( DeleteBehavior.Restrict )
                    .IsRequired( false );

            builder.HasOne( a => a.AuditPeriod )
                    .WithMany()
                    .HasForeignKey( a => a.AuditPeriodId )
                    .OnDelete( DeleteBehavior.Restrict )
                    .IsRequired( false );

            builder.HasOne( a => a.MainTask )
                .WithMany()
                .HasForeignKey( a => a.MainTaskId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();

            builder.HasOne( a => a.TaskType )
                .WithMany()
                .HasForeignKey( a => a.TaskTypeId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();

            builder.HasOne( a => a.Task )
                .WithMany()
                .HasForeignKey( a => a.TaskId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();

        }
    }
}
