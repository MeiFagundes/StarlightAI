using System;
using System.Collections.Generic;

namespace Starlight.EntityExtractors {
    public class PlaySong {

        public static void Fetch(Utterance u) {

            List<String> queryArray = new List<string>(u.Query.Split(" "));
            string[] entityTextArray = { };

            if (queryArray.Contains("play")) {

                if (u.Query.Contains("play the music")) {
                    entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "music", 1);
                    u.Entity.EntityText = String.Join(" ", entityTextArray);
                }
                else if (u.Query.Contains("play the song")) {
                    entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "song", 1);
                    u.Entity.EntityText = String.Join(" ", entityTextArray);
                }
                else {
                    entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "play", 1);
                    u.Entity.EntityText = String.Join(" ", entityTextArray);
                }
            }

            if (u.Query.Contains("listen to") && !u.Query.Contains("listen to music")) {
                entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "to", 1);
                u.Entity.EntityText = String.Join(" ", entityTextArray);
            }
            else if (u.Query.Contains("listen to the music")) {
                entityTextArray = Util.EntityUtil.GetEntityTextArray(queryArray, "music", 1);
                u.Entity.EntityText = String.Join(" ", entityTextArray);
            }

            if (u.Entity.EntityText != String.Empty && u.Entity.EntityText != null) {
                u.Entity.Type = "music";
                Util.EntityUtil.SetEntityIndexes(u, entityTextArray[0]);
            }
        }
    }
}
