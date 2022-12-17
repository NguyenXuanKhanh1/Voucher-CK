using MediatR;

namespace VoucherCK.Api.IoC.NativeInjector
{
    public class PresentationLayerInjector
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMediatR(typeof(Startup));
        }
    }
}
