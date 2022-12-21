using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCK.Application.Repositories
{
    public class BarCodeRedeemRepository : IBarCodeRedeemRepository
    {
        protected readonly CKContext _cKContext;
        public BarCodeRedeemRepository(CKContext cKContext)
        {
            _cKContext = cKContext;
        }

        public async Task<BarCodeRedeems> CreateAsync(BarCodeRedeems entity)
        {
            _cKContext.BarCodeRedeem.Add(entity);
            await _cKContext.SaveChangesAsync();
            return entity;
        }

        public BarCodeRedeems FindBarcodeAsync(string barcode)
        {
            var result = _cKContext.BarCodeRedeem.Where(x => x.Barcode == barcode).FirstOrDefault();
            return result;
        }
    }
}
