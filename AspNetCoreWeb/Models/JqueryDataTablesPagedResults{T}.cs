using System.Collections.Generic;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Models
{
    public class JqueryDataTablesResults<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalSize { get; set; }
    }
}
