using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SETArchitecture.Data.Objects;

namespace SETArchitecture.UI.Models
{
    public class Link
    {
        public int Source { get; set; }
        public int Target { get; set; }
        public int? Value { get; set; }
        public int Weight { get; set; }
    }
}