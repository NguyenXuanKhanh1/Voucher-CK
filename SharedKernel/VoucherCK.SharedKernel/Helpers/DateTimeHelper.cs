namespace VoucherCK.SharedKernel.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertFromTimeZoneInfo(DateTime value)
        {
            TimeZoneInfo timeZoneInfo;

            try
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            }
            catch
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
            }

            var utcValue = value;

            if (value.Kind != DateTimeKind.Utc)
            {
                utcValue = TimeZoneInfo
                    .ConvertTimeToUtc(DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Unspecified),
                        timeZoneInfo);
            }

            return utcValue;
        }

        public static DateTime ConvertToTimeZoneInfo(DateTime value)
        {
            TimeZoneInfo timeZoneInfo;

            try
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            }
            catch
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
            }

            var utcValue = value;

            if (value.Kind == DateTimeKind.Utc)
            {
                utcValue = TimeZoneInfo
                    .ConvertTimeFromUtc(DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Unspecified),
                        timeZoneInfo);
            }

            return utcValue;
        }

        public static DateTime GetStartOfLocalTimeDayInUtc(DateTime dateTime)
        {
            var value = dateTime.Date.AddHours(-7);
            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        public static DateTime GetEndOfLocalTimeDayInUtc(DateTime dateTime)
        {
            var value = dateTime.Date.AddDays(1).Date.AddHours(-7).AddTicks(-1);
            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public static DateTime Now()
        {
            var value = DateTime.UtcNow;
            return ConvertToTimeZoneInfo(value);
        }

        public static DateTime ToUtcDateTime(this DateTime value)
        {
            return ConvertFromTimeZoneInfo(value);
        }

        public static DateTime ToLocalDateTime(this DateTime value)
        {
            return ConvertToTimeZoneInfo(value);
        }


        // DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(promotionResponseData.PromotionStartDate)).DateTime

        public static DateTime FromUnixTimeMilliseconds(string unixTimeStamp)
        {
            var value = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTimeStamp)).DateTime;
            //value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            return value.ToUniversalTime();
        }
    }
}
