using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SETArchitecture.Data.Objects;

namespace SETArchitecture.UI.Models
{
    public class ApplicationDependencies
    {
        public Application Application { get; set; }
        public IEnumerable<Server> Servers { get; set; } 
    }
}