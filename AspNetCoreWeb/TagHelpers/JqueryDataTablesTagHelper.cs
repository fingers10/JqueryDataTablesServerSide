using JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.TagHelpers
{
    [HtmlTargetElement("jquery-datatables", Attributes = "id,class,model")]
    public class JqueryDataTablesTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public object Model { get; set; }

        [HtmlAttributeName("enable-searching")]
        public bool EnableSearching { get; set; }

        [HtmlAttributeName("thead-class")]
        public string TheadClass { get; set; }

        [HtmlAttributeName("search-row-th-class")]
        public string SearchRowThClass { get; set; }

        [HtmlAttributeName("search-input-class")]
        public string SearchInputClass { get; set; }

        [HtmlAttributeName("search-input-style")]
        public string SearchInputStyle { get; set; }

        [HtmlAttributeName("search-input-placeholder-prefix")]
        public string SearchInputPlaceholderPrefix { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.Attributes.Add("id", Id);
            output.Attributes.Add("class", Class);

            output.PreContent.SetHtmlContent($@"<thead class=""{TheadClass}"">");

            var headerRow = new StringBuilder();
            var searchRow = new StringBuilder();

            headerRow.AppendLine("<tr>");

            if (EnableSearching)
            {
                searchRow.AppendLine("<tr>");
            }

            var columns = GetColumnsFromModel(Model.GetType()).Where(c => !c.Exclude).OrderBy(c => c.Order);

            foreach (var column in columns)
            {
                headerRow.AppendLine($"<th>{column.Name}</th>");

                if (!EnableSearching)
                {
                    continue;
                }

                searchRow.AppendLine($@"<th class=""{SearchRowThClass}""><span class=""sr-only"">{column.Name}</span>");

                if (column.HasSearch)
                {
                    searchRow.AppendLine($@"<input type=""search"" style=""{SearchInputStyle}"" class=""{SearchInputClass}"" placeholder=""{SearchInputPlaceholderPrefix} {column.Name}"" aria-label=""{column.Name}"" />");
                }

                searchRow.AppendLine("</th>");
            }

            headerRow.AppendLine("</tr>");
            if (EnableSearching)
            {
                searchRow.AppendLine("</tr>");
            }

            output.Content.SetHtmlContent($"{headerRow.ToString()}{searchRow.ToString()}");
            output.PostContent.SetHtmlContent("</thead>");
        }

        private static IEnumerable<TableColumn> GetColumnsFromModel(Type parentClass)
        {
            var complexProperties = parentClass.GetProperties()
                       .Where(p => p.GetCustomAttributes<NestedSortableAttribute>().Any() || p.GetCustomAttributes<NestedSearchableAttribute>().Any());

            var properties = parentClass.GetProperties();

            foreach (var prop in properties.Except(complexProperties))
            {
                var jqueryDataTableColumn = prop.GetCustomAttribute<JqueryDataTableColumnAttribute>();

                yield return new TableColumn
                {
                    Name = ExpressionHelper.GetPropertyDisplayName(prop),
                    HasSearch = prop.GetCustomAttributes<SearchableAttribute>().Any(),
                    Order = jqueryDataTableColumn != null ? jqueryDataTableColumn.Order : 0,
                    Exclude = jqueryDataTableColumn != null ? jqueryDataTableColumn.Exclude : true
                };
            }

            if (complexProperties.Any())
            {
                foreach (var parentProperty in complexProperties)
                {
                    var parentType = parentProperty.PropertyType;

                    var nestedProperties = GetColumnsFromModel(parentType);

                    foreach (var nestedProperty in nestedProperties)
                    {
                        yield return nestedProperty;
                    }
                }
            }
        }
    }
}
