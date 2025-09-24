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
    public class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType> {
        public void Configure( EntityTypeBuilder<TaskType> builder ) {

            builder.Property( x => x.Code )
                .HasColumnType( "varchar" )
                .HasMaxLength( 255 )
                .IsRequired( true );

            builder.Property( x => x.Description )
                .HasColumnType( "varchar" )
                .HasMaxLength( 255 )
                .IsRequired( true );

            builder.HasOne( a => a.SystemAuditType )
                .WithMany()
                .HasForeignKey( a => a.SystemAuditTypeId )
                .OnDelete( DeleteBehavior.Restrict )
                .IsRequired();
        }
    }
}
