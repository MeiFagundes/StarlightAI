using Starlight.Util;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Starlight.EntityExtractors {
    public class AddReminder {

        public static void Fetch(Utterance u) {

            List<String> queryArray = new List<string>(u.Query.Split(" "));
            string[] entityTextArray = { };

            if (queryArray.Contains("remind")) {

                if (u.Query.Contains("remind me")) {
                    entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "me", 2);
                    u.Entity.EntityText = String.Join(" ", entityTextArray);
                }
                   
                else {
                    entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "remind", 2);
                    u.Entity.EntityText = String.Join(" ", entityTextArray);
                }
            }

            if (u.Query.Contains("add a reminder")) {
                entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "reminder", 2);
                u.Entity.EntityText = String.Join(" ", entityTextArray);
            }

            if (u.Query.Contains("i need")) {
                entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "need", 2);
                u.Entity.EntityText = String.Join(" ", entityTextArray);
            }

            if (u.Entity.EntityText != String.Empty && u.Entity.EntityText != null) {
                u.Entity.Type = "reminder";
                Util.EntityUtil.SetEntityIndexes(u, entityTextArray[0]);
            }

            DateTime? parsedDate;

            Match match = Regex.Match(u.Query, @"(?:(?:0?[0-9]|1[0-2]):[0-5][0-9] [ap]m|(?:[01][0-9]|2[0-3]):[0-5][0-9])", RegexOptions.IgnoreCase);
            if (match.Success) {
                parsedDate = DateTimeUtil.SetDatetimeEntities(match.Value, match.Index, u);
                u.Entity.Type = "reminder";
                u.Entity.DateTime = parsedDate;
            }
            else {

                match = Regex.Match(u.Query, @"(?:(?:0?[0-9]|1[0-2]) [ap]m|(?:[01][0-9]|2[0-3]))", RegexOptions.IgnoreCase);

                if (match.Success) {
                    parsedDate = DateTimeUtil.SetDatetimeEntities(match.Value.Insert(2, ":00"), match.Index, u);
                    u.Entity.Type = "reminder";
                    u.Entity.DateTime = parsedDate;
                }
            }
        }
    }
}
