using MediatR;
using System.Globalization;
using VoucherCK.Application.Configurations;
using VoucherCK.Application.DomainServices.Interfaces;
using VoucherCK.Application.DTOs;
using VoucherCK.Application.Repositories;
using VoucherCK.SharedKernel.Error;
using VoucherCK.SharedKernel.Exceptions;
using VoucherCK.Utility;

namespace VoucherCK.Application.Commands.DeCodeVoucherCommands
{
    public class DecodeVoucherCommandHandler : IRequestHandler<DecodeVoucherCommand, VoucherResultDto>
    {
        private readonly IDecodeVoucherDomainService _decodeVoucherDomainService;
        private readonly IBarCodeRedeemRepository _barCodeRedeemRepository;
        private readonly IApplicationConfiguration _applicationConfiguration;

        public DecodeVoucherCommandHandler(IDecodeVoucherDomainService decodeVoucherDomainService, IBarCodeRedeemRepository barCodeRedeemRepository, IApplicationConfiguration applicationConfiguration)
        {
            _decodeVoucherDomainService = decodeVoucherDomainService;
            _barCodeRedeemRepository = barCodeRedeemRepository;
            _applicationConfiguration = applicationConfiguration;
        }

        public async Task<VoucherResultDto> Handle(DecodeVoucherCommand request, CancellationToken cancellationToken)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            string logContent;
            var currentDate = DateTime.Now;

            var linkFile = _applicationConfiguration.FileResourceConfiguration.FileLog;

            if (request.BarCode.Length != 16)
            {
                throw new ResponseException(NotFoundError.Error(NotFoundErrorEnum.INVALID_BARCODE));
            }
            var result = await _decodeVoucherDomainService.GetVoucherResult(request.BarCode);

            lock (BarCodeRedeems.Lock)
            {
                var checkExisted = _barCodeRedeemRepository.FindBarcodeAsync(request.BarCode);

                if (checkExisted is not null)
                {
                    logContent = $"{currentDate.ToString("yyyy-MM-dd HH:mm:ss")},'{request.BarCode},N/A,Fail,{result.StoreCode},{result.PrizeCode},BarCode in Use,";
                    WriteFileHelper.WriteFileHelperAsync(logContent, linkFile);

                    throw new ResponseException(NotFoundError.Error(NotFoundErrorEnum.BARCODE_EXISTED));
                }

                var fileSource = _applicationConfiguration.FileResourceConfiguration.FileVoucher;

                if (!File.Exists(fileSource))
                {
                    logContent = $"{currentDate.ToString("yyyy-MM-dd HH:mm:ss")},'{request.BarCode},N/A,Fail,{result.StoreCode}," +
                        $"{result.PrizeCode},Voucher File Not Found,";
                    WriteFileHelper.WriteFileHelperAsync(logContent, linkFile);

                    throw new ResponseException(NotFoundError.Error(NotFoundErrorEnum.VOUCHER_RESOURCE_NOTFOUND));
                }

                var lines = File.ReadAllLines(fileSource).ToList();

                if (lines is null || !lines.Any())
                {
                    logContent = $"{currentDate.ToString("yyyy-MM-dd HH:mm:ss")},'{request.BarCode},N/A,Fail,{result.StoreCode}," +
                        $"{result.PrizeCode},No Voucher,";
                    WriteFileHelper.WriteFileHelperAsync(logContent, linkFile);

                    throw new ResponseException(NotFoundError.Error(NotFoundErrorEnum.VOUCHER_NOT_EXIST));
                }

                var voucherData = lines.FirstOrDefault();

                var checkVoucherExisted = _barCodeRedeemRepository.FindVoucherAsync(voucherData);

                if (checkVoucherExisted is not null)
                {
                    lines.Remove(voucherData);
                    File.WriteAllLines(fileSource, lines.ToArray());

                    logContent = $"{currentDate.ToString("yyyy-MM-dd HH:mm:ss")},'{request.BarCode},{voucherData},Fail,{result.StoreCode}," +
                        $"{result.PrizeCode},Voucher duplicate,";
                    WriteFileHelper.WriteFileHelperAsync(logContent, linkFile);

                    throw new ResponseException(NotFoundError.Error(NotFoundErrorEnum.USED_VOUCHER));
                }

                var barCodeRedeem = new BarCodeRedeems(Guid.NewGuid().ToString(), request.BarCode, voucherData, DateTime.UtcNow);
                _barCodeRedeemRepository.CreateAsync(barCodeRedeem);

                lines.Remove(voucherData);
                File.WriteAllLines(fileSource, lines.ToArray());

                result.Voucher = voucherData;

                logContent = $"{currentDate.ToString("yyyy-MM-dd HH:mm:ss")},'{request.BarCode},{voucherData},Success,{result.StoreCode}," +
                        $"{result.PrizeCode},,";
                WriteFileHelper.WriteFileHelperAsync(logContent, linkFile);

                return result;
            }
        }
    }
}
