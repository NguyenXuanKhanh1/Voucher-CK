using VoucherCK.Application;
using VoucherCK.Application.Extensions;

namespace VoucherCK.Api.IoC.NativeInjector
{
    public class ApplicationLayerInjector
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIntegrationCommands();
            services.AddIntegrationApiDomainServices();
        }
    }
}
