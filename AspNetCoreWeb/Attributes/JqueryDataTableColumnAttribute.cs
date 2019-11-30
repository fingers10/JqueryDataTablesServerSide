using System;
using System.Collections.Generic;
using System.Text;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JqueryDataTableColumnAttribute : Attribute
    {
        public int Order { get; set; }
        public bool Exclude { get; set; }
    }
}
