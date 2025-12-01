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
        }
    }
}
