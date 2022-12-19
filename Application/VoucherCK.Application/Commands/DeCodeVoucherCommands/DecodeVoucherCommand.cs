using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherCK.Application.DTOs;

namespace VoucherCK.Application.Commands.DeCodeVoucherCommands
{
    public class DecodeVoucherCommand : IRequest<VoucherResultDto>
    {
        public DecodeVoucherCommand(string barCode, string linkFile)
        {
            BarCode = barCode;
            LinkFile = linkFile;
        }

        public string BarCode { get; }
        public string LinkFile { get; }
    }
}
