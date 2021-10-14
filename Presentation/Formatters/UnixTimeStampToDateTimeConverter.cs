using System;

namespace FreeCMS.Presentation.Formatters
{
    public class UnixTimeStampToDateTimeConverter
    {
        public static DateTime UnixTimeStampToDateTime(uint unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}