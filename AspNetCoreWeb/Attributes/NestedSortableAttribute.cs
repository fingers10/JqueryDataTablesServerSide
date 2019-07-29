using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NestedSortableAttribute : SortableAttribute
    {
        public string ParentEntityProperty { get; set; }
    }
}
