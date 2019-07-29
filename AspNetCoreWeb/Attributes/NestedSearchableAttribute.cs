using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NestedSearchableAttribute : SearchableAttribute
    {
        public string ParentEntityProperty { get; set; }
    }
}
