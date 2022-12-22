using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCK.Application.Repositories
{
    public interface IBarCodeRedeemRepository
    {
        Task<BarCodeRedeems> CreateAsync(BarCodeRedeems entity);
        BarCodeRedeems FindBarcodeAsync(string barcode);
        BarCodeRedeems FindVoucherAsync(string voucher);
    }
}
