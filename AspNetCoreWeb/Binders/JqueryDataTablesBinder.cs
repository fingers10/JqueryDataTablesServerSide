using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Binders
{
    public class JqueryDataTablesBinder:IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var allValues = bindingContext.HttpContext.Request.Query;

            // Retrieve request data
            var draw = Convert.ToInt32(allValues.FirstOrDefault(a => a.Key == "draw").Value);
            var start = Convert.ToInt32(allValues.FirstOrDefault(a => a.Key == "start").Value);
            var length = Convert.ToInt32(allValues.FirstOrDefault(a => a.Key == "length").Value);

            // Search
            var search = new DTSearch {
                Value = allValues.FirstOrDefault(a => a.Key == "search[value]").Value,
                Regex = Convert.ToBoolean(allValues.FirstOrDefault(a => a.Key == "search[regex]").Value)
            };

            // Order
            var o = 0;
            var order = new List<DTOrder>();
            while(allValues.Any(a => a.Key == "order[" + o + "][column]"))
            {
                Enum.TryParse(allValues.FirstOrDefault(a => a.Key == "order[" + o + "][dir]").Value,
                    out DTOrderDir dir);

                order.Add(new DTOrder {
                    Column = Convert.ToInt32(allValues.FirstOrDefault(a => a.Key == "order[" + o + "][column]").Value),
                    Dir = dir
                });
                o++;
            }

            // Columns
            var c = 0;
            var columns = new List<DTColumn>();
            while(allValues.Any(a => a.Key == "columns[" + c + "][name]"))
            {
                columns.Add(new DTColumn {
                    Data = allValues.FirstOrDefault(a => a.Key == "columns[" + c + "][data]").Value,
                    Name = allValues.FirstOrDefault(a => a.Key == "columns[" + c + "][name]").Value,
                    Orderable = Convert.ToBoolean(allValues.FirstOrDefault(a => a.Key == "columns[" + c + "][orderable]").Value),
                    Searchable = Convert.ToBoolean(allValues.FirstOrDefault(a => a.Key == "columns[" + c + "][searchable]").Value),
                    Search = new DTSearch {
                        Value = allValues.FirstOrDefault(a => a.Key == "columns[" + c + "][search][value]").Value,
                        Regex = Convert.ToBoolean(allValues.FirstOrDefault(a => a.Key == "columns[" + c + "][search][regex]").Value)
                    }
                });
                c++;
            }

            // Additional Values
            var p = 0;
            var additionalValues = new List<string>();
            while(allValues.Any(a => a.Key == "additionalValues[" + p + "]"))
            {
                additionalValues.Add(allValues.FirstOrDefault(a => a.Key == "additionalValues[" + p + "]").Value);
                p++;
            }

            var model = new JqueryDataTablesParameters {
                Draw = draw,
                Start = start,
                Length = length,
                Search = search,
                Order = order.ToArray(),
                Columns = columns.ToArray(),
                AdditionalValues = additionalValues.ToArray()
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
