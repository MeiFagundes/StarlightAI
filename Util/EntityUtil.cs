using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starlight.Util {
    public class EntityUtil {
        public static string GetEntityText(List<String> textList, string referenceString, int offset) {
            return String.Join(" ", textList.GetRange(textList.IndexOf(referenceString) + offset, textList.Count - textList.IndexOf(referenceString) - offset).ToArray());
        }

        public static string[] GetEntityTextArray(List<String> textList, string referenceString, int offset) {
            return textList.GetRange(textList.IndexOf(referenceString) + offset, textList.Count - textList.IndexOf(referenceString) - offset).ToArray();
        }

        public static void SetEntityIndexes(Utterance u, String entity) {
            u.Entity.StartIndex = (Byte)u.Query.IndexOf(entity);
            u.Entity.EndIndex = (Byte)(u.Query.Count() - 1);
        }
    }
}
