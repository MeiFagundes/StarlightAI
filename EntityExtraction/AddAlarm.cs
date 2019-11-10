using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace Starlight.EntityExtraction {
    public class AddAlarm {

        public static void Fetch(Utterance u) {


            Match match = Regex.Match(u.Query, @"(?:(?:0?[0-9]|1[0-2]):[0-5][0-9] [ap]m|(?:[01][0-9]|2[0-3]):[0-5][0-9])", RegexOptions.IgnoreCase);
            Console.WriteLine(match.Value);
            if (match.Success) {

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                DateTime parsedDate = DateTime.Parse(match.Value);

                // If the time has already passed today, the alarm will be set for tomorrow.
                if (parsedDate.CompareTo(DateTime.Now) <= 0)
                    parsedDate = parsedDate.AddDays(1);

                if (parsedDate.Date.CompareTo(DateTime.Now.Date) == 0)
                    u.Entity.EntityText = "today";
                else if (parsedDate.Date.CompareTo(DateTime.Now.Date.AddDays(1)) == 0)
                    u.Entity.EntityText = "tomorrow";
                else
                    u.Entity.EntityText = parsedDate.ToString("MMMM d");

                u.Entity.Type = "date";
                u.Entity.StartIndex = (byte) match.Index;
                u.Entity.EndIndex = (byte) (match.Index + 4);
                u.Entity.DateTime = parsedDate;
            }
        }
    }
}
