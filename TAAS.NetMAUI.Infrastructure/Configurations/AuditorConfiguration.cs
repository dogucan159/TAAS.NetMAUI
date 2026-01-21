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
    public class AuditorConfiguration : IEntityTypeConfiguration<Auditor> {
        public void Configure( EntityTypeBuilder<Auditor> builder ) {

            //builder.Property( x => x.IdentificationNumber )
            //    .HasColumnType( "varchar" )
            //    .HasMaxLength( 255 )
            //    .IsRequired( true );

            builder.Property( x => x.FirstName )
                .HasColumnType( "varchar" )
                .HasMaxLength( 255 )
                .IsRequired( true );

            builder.Property( x => x.LastName )
                .HasColumnType( "varchar" )
                .HasMaxLength( 255 )
                .IsRequired( true );
        }
    }
}
