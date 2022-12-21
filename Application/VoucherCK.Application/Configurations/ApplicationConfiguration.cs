using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCK.Application.Configurations
{
    public class ApplicationConfiguration: IApplicationConfiguration
    {
        private readonly IConfiguration _configuration;

        public ApplicationConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FileResourceConfiguration FileResourceConfiguration => _configuration.GetSection("FileResource").Get<FileResourceConfiguration>();
    }
}
