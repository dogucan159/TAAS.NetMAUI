using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Shared;

namespace TAAS.NetMAUI.Infrastructure.Data {
    public static class TaasDbInitializer {

        public static void Initialize( IServiceProvider serviceProvider ) {
            var context = serviceProvider.GetRequiredService<TaasDbContext>();
            context.Database.Migrate();

            if ( !context.Auditors.Any() ) {
                var hashedPassword = PasswordHelper.Hash( "FC_2025.Taas" );
                context.Auditors.Add( new Auditor {
                    Id = 1,
                    FirstName = "Dogucan",
                    LastName = "Kip",
                    IdentificationNumber = "56812118136",
                    Password = hashedPassword
                } );
                context.SaveChanges();
            }

            if ( !context.Settings.Any( b => b.Key == "API_Address" ) ) {
                context.Settings.Add( new Setting {
                    Id = 1,
                    Key = "API_Address",
                    Value = "https://dev-taas.hmb.gov.tr/backend/"
                } );
                context.SaveChanges();
            }

        }
    }
}
