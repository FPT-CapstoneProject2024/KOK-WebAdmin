namespace WebAdmin.DTOModels.Response.Helpers
{
    public class DynamicModelResponse
    {
        public class DynamicModelsResponse<T>
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public PagingMetadata Metadata { get; set; }
            public List<T> Results { get; set; }
        }
        public class PagingMetadata
        {
            public int Page { get; set; }
            public int Size { get; set; }
            public int Total { get; set; }

        }
    }
}
