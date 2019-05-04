using JqueryDataTables.ServerSide.AspNetCoreWeb.Contracts;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure
{
    public class SearchTerm
    {
        public string Name { get; set; }
        public string EntityName { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public bool ValidSyntax { get; set; }
        public ISearchExpressionProvider ExpressionProvider { get; set; }
    }
}
