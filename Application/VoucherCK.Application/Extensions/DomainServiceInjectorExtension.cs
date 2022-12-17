using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VoucherCK.Application.Commands.DeCodeVoucherCommands;
using VoucherCK.Application.DomainServices.Impls;
using VoucherCK.Application.DomainServices.Interfaces;
using VoucherCK.Application.DTOs;

namespace VoucherCK.Application.Extensions
{
    public static class DomainServiceInjectorExtension
    {
        public static void AddIntegrationApiDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IDecodeVoucherDomainService, DecodeVoucherDomainService>();
        }
    }
}
