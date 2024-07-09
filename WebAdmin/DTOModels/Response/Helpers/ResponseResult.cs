namespace WebAdmin.DTOModels.Response.Helpers
{
    public class ResponseResult<T>
    {
        public string? Message { get; set; }
        public T? Value { get; set; }
        public bool? result { get; set; }
    }
}
