using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SETArchitecture.Data.Objects;

namespace SETArchitecture.UI.Models
{
    public class Node
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string GroupName { get; set; }
        public int? Group { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? PX { get; set; }
        public int? PY { get; set; }
        public bool? Fixed { get; set; }
        public int? Weight { get; set; }
    }
}