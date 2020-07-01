using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Extentions
{
    /// <summary>
    /// Extention Methods to convert data table result from list
    /// </summary>
    public static class DataTableExtentions
    {
        public static JqueryDataTablesResult<T> ToDataTable<T>(this IEnumerable<T> list, int draw, int totalCount, int filteredCount)
        {
            return new JqueryDataTablesResult<T>
            {
                Data = list,
                Draw = draw,
                RecordsFiltered = filteredCount,
                RecordsTotal = totalCount
            };
        }
        public static JqueryDataTablesResult<T> ToDataTable<T>(this IEnumerable<T> list, int draw, int totalCount)
        {
            return new JqueryDataTablesResult<T>
            {
                Data = list,
                Draw = draw,
                RecordsFiltered = list.Count(),
                RecordsTotal = totalCount
            };
        }
        public static JqueryDataTablesResult<T> ToDataTable<T>(this IEnumerable<T> list, int draw)
        {
            return new JqueryDataTablesResult<T>
            {
                Data = list,
                Draw = draw,
                RecordsFiltered = list.Count(),
                RecordsTotal = list.Count()
            };
        }
    }
}
