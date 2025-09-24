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
    public class ChecklistAuditorConfiguration : IEntityTypeConfiguration<ChecklistAuditor> {
        public void Configure( EntityTypeBuilder<ChecklistAuditor> builder ) {
            builder.HasOne( x => x.Checklist )
                  .WithMany( a => a.ChecklistAuditors )
                  .HasForeignKey( x => x.ChecklistId )
                  .OnDelete( DeleteBehavior.Cascade )
                  .IsRequired();

            builder.HasOne( x => x.Auditor )
                  .WithMany( a => a.ChecklistAuditors )
                  .HasForeignKey( x => x.AuditorId )
                  .OnDelete( DeleteBehavior.Restrict )
                  .IsRequired();
        }
    }
}
