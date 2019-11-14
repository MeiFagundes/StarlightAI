using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Starlight.Util {
    public class DateTimeUtil {
        public static DateTime? SetDatetimeEntities(String value, int index, Utterance u) {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            DateTime parsedDate = DateTime.Parse(value);

            // If the time has already passed today, the alarm will be set for tomorrow.
            if (parsedDate.CompareTo(DateTime.Now) <= 0)
                parsedDate = parsedDate.AddDays(1);

            u.Entity.StartIndex = (byte)index;
            u.Entity.EndIndex = (byte)(index + 4);

            return parsedDate;
        }
    }
}
