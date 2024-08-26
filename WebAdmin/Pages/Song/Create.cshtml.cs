using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Net.Http.Headers;
using WebAdmin.Context;
using WebAdmin.DTOModels.Request.Artist;
using WebAdmin.DTOModels.Request.Song;
using WebAdmin.DTOModels.Response;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Interfaces;
namespace WebAdmin.Pages.Song
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;

        private string? imageUrl { get; set; }
        public IApiClient apiClient { get; set; }

        [BindProperty]
        public DTOModels.Request.Song.CreateSongRequestModel Song { get; set; } = new DTOModels.Request.Song.CreateSongRequestModel();
        public static List<SongArtist> SongIds { get; set; } = new List<SongArtist>();
        public static List<SongSinger> SingerIds { get; set; } = new List<SongSinger>();
        public static List<SongGenre> GenreIds { get; set; } = new List<SongGenre>();
        public List<DTOModels.Response.Artist> SearchArtistResults { get; set; } = new List<DTOModels.Response.Artist>();
        public List<DTOModels.Response.Singer> SearchSingerResults { get; set; } = new List<DTOModels.Response.Singer>();
        public List<DTOModels.Response.Genre> SearchGenreResults { get; set; } = new List<DTOModels.Response.Genre>();

        [BindProperty]
        public Guid SelectedArtistId { get; set; }
        
        [BindProperty]
        public Guid SelectedSingerId { get; set; }
        
        [BindProperty]
        public Guid SelectedGenreId { get; set; }

        public CreateModel(IApiClient apiClient, IHttpClientFactory clientFactory, IMapper mapper)
        {
            this.apiClient = apiClient;
            _clientFactory = clientFactory;
            _mapper = mapper;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return Page();
                }
                Song.CreatorId = LoginModel.AccountId.Value;                
                Song.SongArtists = _mapper.Map<ICollection<SongArtistRequestModel>>(SongIds);
                Song.SongSingers = _mapper.Map<ICollection<SongSingerRequestModel>>(SingerIds);
                Song.SongGenres = _mapper.Map<ICollection<SongGenreRequestModel>>(GenreIds);

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SongResource;

                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(uri+"/post-url-video");

                //    using (var form = new MultipartFormDataContent())
                //    {
                        
                //        if (Song.SongFile != null)
                //        {
                //            var fileContent = new StreamContent(Song.SongFile.OpenReadStream());
                //            fileContent.Headers.ContentType = new MediaTypeHeaderValue(Song.SongFile.ContentType);
                //            form.Add(fileContent, "file", Song.SongFile.FileName);
                //        }


                //        var responseUrl = await apiClient.PostAsync(uri + "/post-url-video?file=" + Song.SongFile, form);
                //        var jsonResponseUrl = await responseUrl.Content.ReadAsStringAsync();

                //        var dataUrl = JsonConvert.DeserializeObject<(bool, string?)>(jsonResponseUrl);


                       CreateSongRequestModel1 dataSong = _mapper.Map<CreateSongRequestModel1>(Song);

                //        dataSong.SongUrl = dataUrl.Item2;

                using (var memoryStream = new MemoryStream())
                {
                    await Song.SongFile.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    dataSong.SongUrl =  Convert.ToBase64String(fileBytes);

                    var response = await apiClient.PostAsync(uri, dataSong);

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Song>>(jsonResponse);

                    Song = new CreateSongRequestModel();

                    if (data.result.Value)
                    {
                        return RedirectToPage("./Index");
                    }
                }


                //byte[] fileBytes1 = Convert.FromBase64String(dataSong.SongUrl);

                //var stream = new MemoryStream(fileBytes1);

                //var dataUrl = new FormFile(stream, 0, stream.Length, "file", "file")
                //{
                //    Headers = new HeaderDictionary(),
                //    ContentType = "video/mp4"
                //};



                //    }
                //}



            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
            return null;
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

        public void OnPostAddArtist()
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
        }

        public void OnPostAddSinger()
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
        }

        public void OnPostAddGerne()
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
        }
        //public IActionResult OnPostAddArtist()
        //{
        //    string value = Request.Form["submit"];
        //    if (value.Equals("Bỏ chọn"))
        //    {
        //        GenreIds.Remove(GenreIds.Find(x => x.GenreId == SelectedArtistId));
        //    }
        //    else
        //    {
        //        SongIds.Add(new SongArtist() { ArtistId = SelectedArtistId });
        //        SearchArtistResults.RemoveAll(a => a.ArtistId == SelectedArtistId);
        //    }

        //    Return JSON response
        //    return new JsonResult(new { success = true, message = "Artist added/removed successfully." });
        //}

        //public IActionResult OnPostAddSinger()
        //{
        //    string value = Request.Form["submit"];
        //    if (value.Equals("Bỏ chọn"))
        //    {
        //        SingerIds.Remove(SingerIds.Find(x => x.SingerId == SelectedSingerId));
        //    }
        //    else
        //    {
        //        SingerIds.Add(new SongSinger() { SingerId = SelectedSingerId });
        //        SearchSingerResults.RemoveAll(a => a.SingerId == SelectedSingerId);
        //    }

        //    Return JSON response
        //    return new JsonResult(new { success = true, message = "Singer added/removed successfully." });
        //}

        //public IActionResult OnPostAddGerne()
        //{
        //    string value = Request.Form["submit"];
        //    if (value.Equals("Bỏ chọn"))
        //    {
        //        GenreIds.Remove(GenreIds.Find(x => x.GenreId == SelectedGenreId));
        //    }
        //    else
        //    {
        //        GenreIds.Add(new DTOModels.Response.SongGenre() { GenreId = SelectedGenreId });
        //        SearchGenreResults.RemoveAll(a => a.GenreId == SelectedGenreId);
        //    }

        //    Return JSON response
        //    return new JsonResult(new { success = true, message = "Genre added/removed successfully." });
        //}

    }
}
