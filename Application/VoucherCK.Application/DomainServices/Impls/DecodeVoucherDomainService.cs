using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VoucherCK.Application.DomainServices.Interfaces;
using VoucherCK.Application.DTOs;
using VoucherCK.SharedKernel.Error;
using VoucherCK.SharedKernel.Exceptions;
using VoucherCK.Utility;

namespace VoucherCK.Application.DomainServices.Impls
{
    public class DecodeVoucherDomainService : IDecodeVoucherDomainService
    {
        public string pathString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DeCode.dll");

        [DllImport("DeCode.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "deCode16", CharSet = CharSet.Ansi)]
        public static extern int deCode16(string serial, StringBuilder rtrStore, StringBuilder rtrPrizeCode, StringBuilder rtrNPlay);

        public async Task<VoucherResultDto> GetVoucherResult(string barcode)
        {
            var currentDate = DateTime.Now;

            StringBuilder rtrStore = new StringBuilder(256);
            StringBuilder rtrPrizeCode = new StringBuilder(256);
            StringBuilder rtrNPlay = new StringBuilder(256);

            var deCodeVoucherResult = deCode16(barcode, rtrStore, rtrPrizeCode, rtrNPlay);

            string logContent;

            if (deCodeVoucherResult != 0)
            {
                logContent = $"[{currentDate.ToString("yyyy-MM-dd HH:mm:ss")}]  BarCode: {barcode} is not applied promotion";

                logContent = string.Format(logContent, barcode);
                await WriteFileHelper.WriteFileHelperAsync(logContent);
                throw new ResponseException(NotFoundError.Error(NotFoundErrorEnum.VOUCHER_NOTFOUND));
            }

            var result = new VoucherResultDto
            {
                BarCode = barcode,
                StoreCode = rtrStore.ToString(),
                PrizeCode = rtrPrizeCode.ToString(),
                IsWarrior = Convert.ToInt32(rtrNPlay.ToString()),
                Result = 0
            };

            logContent = $"[{currentDate.ToString("yyyy-MM-dd HH:mm:ss")}]  BarCode: {barcode} is applied promotion at StoreCode - {result.StoreCode}";

            await WriteFileHelper.WriteFileHelperAsync(logContent);

            return result;
        }
    }
}
