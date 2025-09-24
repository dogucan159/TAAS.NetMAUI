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
    public class InstitutionConfiguration : IEntityTypeConfiguration<Institution> {
        public void Configure( EntityTypeBuilder<Institution> builder ) {
            builder.Property( x => x.Code )
                .HasColumnType( "text" )
                .IsRequired( true );

            builder.Property( x => x.RegisteredName )
                .HasColumnType( "text" )
                .IsRequired( true );
        }
    }
}
