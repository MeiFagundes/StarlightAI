using Starlight.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace Starlight.EntityExtraction {
    public class AddAlarm {

        public static void Fetch(Utterance u) {

            DateTime? parsedDate;

            Match match = Regex.Match(u.Query, @"(?:(?:0?[0-9]|1[0-2]):[0-5][0-9] [ap]m|(?:[01][0-9]|2[0-3]):[0-5][0-9])", RegexOptions.IgnoreCase);
            if (match.Success) {
                parsedDate = DateTimeUtil.SetDatetimeEntities(match.Value, match.Index, u);
                u.Entity.Type = "date";
                u.Entity.DateTime = parsedDate;
                u.Entity.EntityText = GetEntityText(parsedDate);
            }
            else {

                match = Regex.Match(u.Query, @"(?:(?:0?[0-9]|1[0-2]) [ap]m|(?:[01][0-9]|2[0-3]))", RegexOptions.IgnoreCase);

                if (match.Success) {
                    parsedDate = DateTimeUtil.SetDatetimeEntities(match.Value.Insert(2, ":00"), match.Index, u);
                    u.Entity.Type = "date";
                    u.Entity.DateTime = parsedDate;
                    u.Entity.EntityText = GetEntityText(parsedDate);
                }
            }
        }

        static String GetEntityText(DateTime? parsedDate) {
            if (parsedDate?.Date.CompareTo(DateTime.Now.Date) == 0)
                return "today";
            else if (parsedDate?.Date.CompareTo(DateTime.Now.Date.AddDays(1)) == 0)
                return "tomorrow";
            else
                return parsedDate?.ToString("MMMM d");
        }
    }
}
