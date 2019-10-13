using Fingers10.ExcelExport.ActionResults;
using System.Collections.Generic;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.ActionResults
{
    public class JqueryDataTablesExcelResult<T> : ExcelResult<T> where T : class
    {
        public JqueryDataTablesExcelResult(IEnumerable<T> data, string sheetName, string fileName) : base(data, sheetName, fileName)
        {
        }
    }
}