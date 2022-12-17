using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCK.Application
{
    public interface IExceptionHandler
    {
        Task Handle(HttpContext context, Exception exception);
    }
}
