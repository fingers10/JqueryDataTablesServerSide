using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SortableAttribute:Attribute
    {
        public string EntityProperty { get; set; }

        public bool Default { get; set; }
    }
}
