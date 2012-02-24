using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SETArchitecture.Data.Objects;

namespace SETArchitecture.UI.Models
{
    public class ArchitectureUpdateModel
    {
        public Application Application { get; set; }
        public Server Server { get; set; }
    }
}