using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace VoucherCK.Resource
{
    public class ErrorMessageFactory : ErrorMessages
    {
        public static string? GetNamedMessage(string name)
        {
            return ResourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture) ?? GeneralNotDefinedError;
        }

        public static string GetNamedMessage(string name, CultureInfo? culture)
        {
            return ResourceManager.GetString(name, culture ?? Culture) ?? ResourceManager.GetString("GeneralNotDefinedError", culture ?? Culture) ?? UNDEFINED_MESSAGE;
        }

        public static string GetNamedMessage(Enum code)
        {
            return ResourceManager.GetString(code.ToString(), Thread.CurrentThread.CurrentUICulture) ?? ResourceManager.GetString("GeneralNotDefinedError", Thread.CurrentThread.CurrentUICulture) ?? UNDEFINED_MESSAGE;
        }

        public static string GetNamedMessage(Enum code, CultureInfo? culture)
        {
            return ResourceManager.GetString(code.ToString(), Culture) ?? ResourceManager.GetString("GeneralNotDefinedError", culture ?? Culture) ?? UNDEFINED_MESSAGE;
        }
    }
}
