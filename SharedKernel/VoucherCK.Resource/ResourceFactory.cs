using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCK.Resource
{
    public class ResourceFactory : MainResource
    {
        public static string? GetNamedResources(string name, CultureInfo? cultureInfo = null)
        {
            return ResourceManager.GetString(name, cultureInfo ?? Culture);
        }
    }
}
