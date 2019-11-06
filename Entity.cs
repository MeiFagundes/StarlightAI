using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight {
    public class Entity {

        public string EntityText { get; set; }
        public string Type { get; set; }
        public Byte startIndex { get; set; }
        public Byte endIndex { get; set; }

        public Entity() {

        }

        public Entity(string entityText) : base() {
            EntityText = entityText;
        }
    }
}
