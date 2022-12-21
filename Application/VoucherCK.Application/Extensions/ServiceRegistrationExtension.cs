using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherCK.Application.Repositories;

namespace VoucherCK.Application.Extensions
{
    public static class ServiceRegistrationExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IBarCodeRedeemRepository, BarCodeRedeemRepository>()
                ;
        }
    }
}
