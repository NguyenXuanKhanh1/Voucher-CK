namespace VoucherCK.Api.IoC.NativeInjector
{
    public class NativeInjector
    {
        public static void Register(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            ApplicationLayerInjector.Register(services, configuration);
            PresentationLayerInjector.Register(services, configuration);
        }
    }
}
