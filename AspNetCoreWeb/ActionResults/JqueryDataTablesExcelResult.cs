using JqueryDataTables.ServerSide.AspNetCoreWeb.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.ActionResults
{
    public class JqueryDataTablesExcelResult<T>:IActionResult
    {
        private readonly IEnumerable<T> _data;

        public JqueryDataTablesExcelResult(IEnumerable<T> data,string sheetName,string fileName)
        {
            _data = data;
            SheetName = sheetName;
            FileName = fileName;
        }

        public string SheetName { get; }
        public string FileName { get; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            try
            {
                var excelBytes = await _data.GenerateExcelForDataTableAsync(SheetName);
                WriteExcelFileAsync(context.HttpContext,excelBytes);

            } catch(Exception e)
            {
                Console.WriteLine(e);

                var errorBytes = await new List<T>().GenerateExcelForDataTableAsync(SheetName);
                WriteExcelFileAsync(context.HttpContext,errorBytes);
            }
        }

        private async void WriteExcelFileAsync(HttpContext context,byte[] bytes)
        {
            context.Response.Headers["content-disposition"] = $"attachment; filename={FileName}.xlsx";
            await context.Response.Body.WriteAsync(bytes,0,bytes.Length);
        }
    }
}
