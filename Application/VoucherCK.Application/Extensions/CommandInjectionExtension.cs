using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VoucherCK.Application.Commands.DeCodeVoucherCommands;
using VoucherCK.Application.DTOs;

namespace VoucherCK.Application.Extensions
{
    public static class CommandInjectionExtension
    {
        public static void AddIntegrationCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<DecodeVoucherCommand, VoucherResultDto>, DecodeVoucherCommandHandler>(); 
        }
    }
}
