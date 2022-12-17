using MediatR;
using System.Globalization;
using VoucherCK.Application.DomainServices.Interfaces;
using VoucherCK.Application.DTOs;
using VoucherCK.SharedKernel.Error;
using VoucherCK.SharedKernel.Exceptions;
using VoucherCK.Utility;

namespace VoucherCK.Application.Commands.DeCodeVoucherCommands
{
    public class DecodeVoucherCommandHandler : IRequestHandler<DecodeVoucherCommand, VoucherResultDto>
    {
        private readonly IDecodeVoucherDomainService _decodeVoucherDomainService;

        public DecodeVoucherCommandHandler(IDecodeVoucherDomainService decodeVoucherDomainService)
        {
            _decodeVoucherDomainService = decodeVoucherDomainService;
        }

        public async Task<VoucherResultDto> Handle(DecodeVoucherCommand request, CancellationToken cancellationToken)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            if (request.BarCode.Length != 16)
            {
                var currentDate = DateTime.Now;
                var logContent = $"[{currentDate.ToString("yyyy-MM-dd HH:mm:ss")}]  BarCode: {request.BarCode} is invalid format";
                await WriteFileHelper.WriteFileHelperAsync(logContent);

                throw new ResponseException(NotFoundError.Error(NotFoundErrorEnum.INVALID_BARCODE));
            }
            var result = await _decodeVoucherDomainService.GetVoucherResult(request.BarCode);
            return result;
        }
    }
}
