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
    public class SpecificFunctionConfiguration : IEntityTypeConfiguration<SpecificFunction> {
        public void Configure( EntityTypeBuilder<SpecificFunction> builder ) {
            builder.Property( x => x.Code )
                .HasColumnType( "varchar" )
                .HasMaxLength( 255 )
                .IsRequired( true );

            builder.Property( x => x.GeneralCode )
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
