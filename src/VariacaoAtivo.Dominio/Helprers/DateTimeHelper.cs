using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariacaoAtivo.Dominio.Helprers
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertTimestampToDateTime(long timestamp) 
        {
            var date = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;      
            
            return date;
        }

        public static long ConvertToTimestamp(this DateTime date)
        {
            DateTimeOffset dateTimeOffset = DateTime.SpecifyKind(date.ToLocalTime(),DateTimeKind.Local);

            return dateTimeOffset.ToUnixTimeSeconds();
        }
    }
}
