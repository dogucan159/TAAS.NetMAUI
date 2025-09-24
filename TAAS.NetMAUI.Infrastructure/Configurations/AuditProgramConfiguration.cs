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
    public class AuditProgramConfiguration : IEntityTypeConfiguration<AuditProgram> {
        public void Configure( EntityTypeBuilder<AuditProgram> builder ) {
            builder.HasOne( b => b.Institution )
                    .WithMany()
                    .HasForeignKey( b => b.InstitutionId )
                    .OnDelete( DeleteBehavior.Restrict )
                    .IsRequired( false );

            builder.HasOne( b => b.KeyRequirement )
                    .WithMany()
                    .HasForeignKey( b => b.KeyRequirementId )
                    .OnDelete( DeleteBehavior.Restrict )
                    .IsRequired( false );

            builder.HasOne( b => b.SpecificFunction )
                    .WithMany()
                    .HasForeignKey( b => b.SpecificFunctionId )
                    .OnDelete( DeleteBehavior.Restrict )
                    .IsRequired( false );
        }
    }
}
