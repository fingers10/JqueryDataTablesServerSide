namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure
{
    public class SortTerm
    {
        public string Name { get; set; }
        public string EntityName { get; set; }
        public bool Descending { get; set; }
        public bool Default { get; set; }
        public string ParentName { get; set; }
        public string ParentEntityName { get; set; }
        public bool HasNavigation { get; set; }
    }
}
