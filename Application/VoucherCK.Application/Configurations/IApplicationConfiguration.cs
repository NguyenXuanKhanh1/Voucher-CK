using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCK.Application.Configurations
{
    public interface IApplicationConfiguration
    {
        FileResourceConfiguration FileResourceConfiguration { get; }
    }
}
