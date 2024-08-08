namespace WebAdmin.DTOModels.Response.Helpers
{
    public class DashboardResponse<TEntity>
    {
        public string? Message { get; set; }
        public Dictionary<TEntity, decimal>? Values { get; set; }
    }
}
