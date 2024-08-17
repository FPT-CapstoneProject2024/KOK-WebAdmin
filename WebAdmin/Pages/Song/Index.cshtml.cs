using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Song
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IApiClient apiClient;

        [BindProperty]
        public DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Song> data { get; set; } = new DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Song>();
        [BindProperty]
        public int PageSzie { get; set; } = 3;
        [BindProperty]
        public int CurrentPage { get; set; }
        [BindProperty]
        public static int TotalPage { get; set; } = 1;
        public static List<SongArtist> SongIds { get; set; } = new List<SongArtist>();
        public static List<SongSinger> SingerIds { get; set; } = new List<SongSinger>();
        public static List<SongGenre> GenreIds { get; set; } = new List<SongGenre>();
        public List<DTOModels.Response.Artist> SearchArtistResults { get; set; } = new List<DTOModels.Response.Artist>();
        public List<DTOModels.Response.Singer> SearchSingerResults { get; set; } = new List<DTOModels.Response.Singer>();
        public List<DTOModels.Response.Genre> SearchGenreResults { get; set; } = new List<DTOModels.Response.Genre>();

        //sorry code do
        public List<SongArtist> SongIds2 = SongIds;
        public List<SongSinger> SingerId2 = SingerIds;
        public List<SongGenre> GenreId2 = GenreIds;

        [BindProperty]
        public Guid SelectedArtistId { get; set; }

        [BindProperty]
        public Guid SelectedSingerId { get; set; }

        [BindProperty]
        public Guid SelectedGenreId { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IApiClient apiClient)
        {
            this.logger = logger;
            this.apiClient = apiClient;
        }

        public async Task<IActionResult> OnGet(string? number = null, string? filter = null)
        {

            CurrentPage = int.Parse(number ?? "1");
            CurrentPage = (CurrentPage < 1) ? 1 : CurrentPage;
            CurrentPage = (CurrentPage > TotalPage) ? TotalPage : CurrentPage;

            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SongResource;

            var response = await apiClient.GetAsync(uri + "?page=" + CurrentPage + filter);



            var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Song>>(jsonResponse);
#pragma warning restore CS8601 // Possible null reference assignment.

            if (data.Results is not null)
            {
                TotalPage = (int)MathF.Ceiling((float)data.Metadata.Total / (float)data.Metadata.Size);
            }
            return Page();
        }


        public async Task<IActionResult> OnPostSearch()
        {
            string? filter = Request.Form["txt_filter"];
            string? search = Request.Form["txt_search"];
            return await OnGet(filter: "&" + filter + "=" + search);

        }

        public IActionResult OnGetSearchArtist(string query)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ArtistResource + "?ArtistName=" + query;
            var response = apiClient.GetAsync(uri).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            SearchArtistResults = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Artist>>(jsonResponse)?.Results;

            return Partial("_SearchArtistResults", this);


        }

        public IActionResult OnGetSearchSinger(string query)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SingerResource + "?SingerName=" + query;
            var response = apiClient.GetAsync(uri).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            SearchSingerResults = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Singer>>(jsonResponse)?.Results;

            return Partial("_SearchSingerResults", this);


        }

        public IActionResult OnGetSearchGenre(string query)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.GenreResource + "?GenreName=" + query;
            var response = apiClient.GetAsync(uri).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            SearchGenreResults = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Genre>>(jsonResponse)?.Results;

            return Partial("_SearchGenreResults", this);


        }

        /*public void OnPostAddArtist()
        {
            string value = Request.Form["submit"];
            if (value.Equals("B? ch?n"))
            {
                GenreIds.Remove(GenreIds.Find(x => x.GenreId == SelectedArtistId));
            }
            else
            {
                SongIds.Add(new SongArtist() { ArtistId = SelectedArtistId });

                SearchArtistResults.RemoveAll(a => a.ArtistId == SelectedArtistId);
            }
        }
        
        public void OnPostAddSinger()
        {
            string value = Request.Form["submit"];
            if(value.Equals("B? ch?n"))
            {
                SingerIds.Remove(SingerIds.Find(x => x.SingerId == SelectedSingerId));
            }
            else
            {
                SingerIds.Add(new SongSinger() { SingerId = SelectedSingerId });

                SearchSingerResults.RemoveAll(a => a.SingerId == SelectedSingerId);
            }
        }
        
        public void OnPostAddGerne()
        {
            string value = Request.Form["submit"];
            if (value.Equals("B? ch?n"))
            {
                GenreIds.Remove(GenreIds.Find(x => x.GenreId == SelectedGenreId));
            }
            else
            {
                GenreIds.Add(new DTOModels.Response.SongGenre() { GenreId = SelectedGenreId });

                SearchGenreResults.RemoveAll(a => a.GenreId == SelectedGenreId);
            }
        }*/
        public IActionResult OnPostAddArtist()
        {
            string value = Request.Form["submit"];
            if (value.Equals("Bỏ chọn"))
            {
                GenreIds.Remove(GenreIds.Find(x => x.GenreId == SelectedArtistId));
            }
            else
            {
                SongIds.Add(new SongArtist() { ArtistId = SelectedArtistId });
                SearchArtistResults.RemoveAll(a => a.ArtistId == SelectedArtistId);
            }

            // Return JSON response
            return new JsonResult(new { success = true, message = "Artist added/removed successfully." });
            //return Partial("_SearchArtistResults", this);
        }

        public IActionResult OnPostAddSinger()
        {
            string value = Request.Form["submit"];
            if (value.Equals("Bỏ chọn"))
            {
                SingerIds.Remove(SingerIds.Find(x => x.SingerId == SelectedSingerId));
            }
            else
            {
                SingerIds.Add(new SongSinger() { SingerId = SelectedSingerId });
                SearchSingerResults.RemoveAll(a => a.SingerId == SelectedSingerId);
            }

            // Return JSON response
            return new JsonResult(new { success = true, message = "Singer added/removed successfully." });
        }

        public IActionResult OnPostAddGerne()
        {
            string value = Request.Form["submit"];
            if (value.Equals("Bỏ chọn"))
            {
                GenreIds.Remove(GenreIds.Find(x => x.GenreId == SelectedGenreId));
            }
            else
            {
                GenreIds.Add(new DTOModels.Response.SongGenre() { GenreId = SelectedGenreId });
                SearchGenreResults.RemoveAll(a => a.GenreId == SelectedGenreId);
            }

            // Return JSON response
            return new JsonResult(new { success = true, message = "Genre added/removed successfully." });
        }
    }
}
