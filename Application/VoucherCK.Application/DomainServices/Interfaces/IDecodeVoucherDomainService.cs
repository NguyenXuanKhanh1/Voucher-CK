using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherCK.Application.DTOs;

namespace VoucherCK.Application.DomainServices.Interfaces
{
    public interface IDecodeVoucherDomainService
    {
        Task<VoucherResultDto> GetVoucherResult(string barcode);
    }
}
