using System.Globalization;

namespace ChargeHub.Resource;

public class ResourceFactory : MainResource
{
    public static string? GetNamedResources(string name, CultureInfo? cultureInfo = null)
    {
        return ResourceManager.GetString(name, cultureInfo ?? Culture);
    }
}