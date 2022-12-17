using System.Globalization;

namespace ChargeHub.Resource;

public class LabelFactory : Label
{
    public static string GetLabel(string name)
    {
        return ResourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture) ??
               ResourceManager.GetString(name, Culture) ?? ErrorMessages.GeneralNotDefinedError;
    }

    public static string GetLabel(string name, CultureInfo? culture)
    {
        return ResourceManager.GetString(name, culture ?? Culture) ??
               ErrorMessageFactory.GetNamedMessage("GeneralNotDefinedError", culture ?? Culture);
    }

    public static string GetLabel(Enum code)
    {
        return ResourceManager.GetString(code.ToString(), Culture) ?? code.ToString();
    }
}