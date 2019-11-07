using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight {
    public class Entity {

        public String EntityText { get; set; }
        public String Type { get; set; }
        public Byte startIndex { get; set; }
        public Byte endIndex { get; set; }

        public Entity() {

        }

        public Entity(string entityText) : base() {
            EntityText = entityText;
        }
    }
}
