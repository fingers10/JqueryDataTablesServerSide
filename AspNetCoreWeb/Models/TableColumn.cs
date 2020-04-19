namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Models
{
    public class TableColumn
    {
        public string Name { get; set; }
        public bool HasSearch { get; set; }
        public int Order { get; set; }
        public bool Exclude { get; set; }
        public string Type { get; set; }
    }
}
