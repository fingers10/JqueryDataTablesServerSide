using Fingers10.ExcelExport.ActionResults;
using System.Collections.Generic;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.ActionResults
{
    public class JqueryDataTablesCSVResult<T> : CSVResult<T> where T : class
    {
        public JqueryDataTablesCSVResult(IEnumerable<T> data, string fileName) : base(data, fileName)
        {
        }
    }
}
