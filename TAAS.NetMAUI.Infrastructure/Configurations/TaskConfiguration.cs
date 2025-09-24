using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;

namespace TAAS.NetMAUI.Infrastructure {
    public class TaskConfiguration : IEntityTypeConfiguration<Core.Entities.Task> {
        public void Configure( EntityTypeBuilder<Core.Entities.Task> builder ) {

            builder.Property( x => x.Code )
                .HasColumnType( "varchar" )
                .HasMaxLength( 255 )
                .IsRequired( true );

            builder.Property( x => x.Description )
                .HasColumnType( "varchar" )
                .HasMaxLength( 255 )
                .IsRequired( true );
        }
    }
}
