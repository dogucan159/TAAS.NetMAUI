using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Infrastructure.Data {
    internal class TaasDbContextFactory : IDesignTimeDbContextFactory<TaasDbContext> {
        public TaasDbContext CreateDbContext( string[] args ) {
            var optionsBuilder = new DbContextOptionsBuilder<TaasDbContext>();

            var dbPath = Path.Combine(
                Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ),
                "taas_offline.db"
            );

            optionsBuilder.UseSqlite( $"Data Source={dbPath}" );

            return new TaasDbContext( optionsBuilder.Options );

        }
    }
}
