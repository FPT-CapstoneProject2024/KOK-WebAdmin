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
        public CreateSongRequestModel Song { get; set; } = new DTOModels.Request.Song.CreateSongRequestModel();
        public CreateSongRequestModel1 Song1 { get; set; } = new DTOModels.Request.Song.CreateSongRequestModel1();
        //public static List<SongArtist> SongIds { get; set; } = new List<SongArtist>();
        //public static List<SongSinger> SingerIds { get; set; } = new List<SongSinger>();
        //public static List<SongGenre> GenreIds { get; set; } = new List<SongGenre>();

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

        /*public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return Page();
                }
                Song.CreatorId = HttpContext.Session.GetString("AccountId").Value;                
                //Song.SongArtists = _mapper.Map<ICollection<SongArtistRequestModel>>(SongIds);
                //Song.SongSingers = _mapper.Map<ICollection<SongSingerRequestModel>>(SingerIds);
                //Song.SongGenres = _mapper.Map<ICollection<SongGenreRequestModel>>(GenreIds);

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
            finally
            {
                Song = new CreateSongRequestModel();
            }
            return null;
        }*/
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                // Deserialize selected items from hidden fields
                var selectedArtistsJson = Request.Form["selectedArtistsJson"].ToString();
                var selectedSingersJson = Request.Form["selectedSingersJson"].ToString();
                var selectedGenresJson = Request.Form["selectedGenresJson"].ToString();

                // Transform the JSON array of arrays into List<SongArtistRequestModelExtended>
                var songArtists = TransformJsonArrayToSongArtistRequestModelExtended(selectedArtistsJson);
                var songSingers = TransformJsonArrayToSongSingerRequestModelExtended(selectedSingersJson);
                var songGenres = TransformJsonArrayToSongGenreRequestModelExtended(selectedGenresJson);

                // Prepare request model
                var dataSong = _mapper.Map<CreateSongRequestModel1>(Song);
                dataSong.SongArtists = songArtists;
                dataSong.SongSingers = songSingers;
                dataSong.SongGenres = songGenres;
                dataSong.SongUrl = null; // This will be set later
                dataSong.CreatorId = (Guid)(JsonConvert.DeserializeObject<DTOModels.Response.Account>(HttpContext.Request.Cookies["AccountData"])?.AccountId);

                // Handle file upload
                if (Song.SongFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Song.SongFile.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        dataSong.SongUrl = Convert.ToBase64String(fileBytes);
                    }
                }

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SongResource;
                var response = await apiClient.PostAsync(uri, dataSong);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Song>>(jsonResponse);

                // Clear the Song object for new input
                Song = new CreateSongRequestModel();

                if (data.result.Value)
                {
                    return new JsonResult(new { success = true });
                }
            }
            catch (Exception)
            {
                return new JsonResult(new { success = false });
            }
            finally
            {
                Song = new CreateSongRequestModel();
            }
            return null;
        }

        // Helper methods to transform JSON array of arrays into List<T> for each extended model
        private List<SongArtistRequestModelExtended> TransformJsonArrayToSongArtistRequestModelExtended(string json)
        {
            var artistArrays = JsonConvert.DeserializeObject<List<List<object>>>(json) ?? new List<List<object>>();
            return artistArrays.Select(a => new SongArtistRequestModelExtended
            {
                ArtistId = Guid.Parse(a[0].ToString()),  // Assuming the ID is the first item and it's a GUID
                ArtistName = a[1].ToString()            // Assuming the name is the second item
            }).ToList();
        }

        private List<SongSingerRequestModelExtended> TransformJsonArrayToSongSingerRequestModelExtended(string json)
        {
            var singerArrays = JsonConvert.DeserializeObject<List<List<object>>>(json) ?? new List<List<object>>();
            return singerArrays.Select(s => new SongSingerRequestModelExtended
            {
                SingerId = Guid.Parse(s[0].ToString()),  // Assuming the ID is the first item and it's a GUID
                SingerName = s[1].ToString()            // Assuming the name is the second item
            }).ToList();
        }

        private List<SongGenreRequestModelExtended> TransformJsonArrayToSongGenreRequestModelExtended(string json)
        {
            var genreArrays = JsonConvert.DeserializeObject<List<List<object>>>(json) ?? new List<List<object>>();
            return genreArrays.Select(g => new SongGenreRequestModelExtended
            {
                GenreId = Guid.Parse(g[0].ToString()),  // Assuming the ID is the first item and it's a GUID
                GenreName = g[1].ToString()            // Assuming the name is the second item
            }).ToList();
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
                CreateSongRequestModel.SongArtists.Remove(CreateSongRequestModel.SongArtists.FirstOrDefault(x => x.ArtistId == SelectedArtistId));
            }
            else
            {
                CreateSongRequestModel.SongArtists.Add(new SongArtistRequestModel() { ArtistId = SelectedArtistId });

                SearchArtistResults.RemoveAll(a => a.ArtistId == SelectedArtistId);
            }
        }

        //public void OnPostAddSinger(Guid singerId, string action)
        //{
        //     Logic để thêm hoặc bỏ chọn ca sĩ
       
        //    var singer = Song.SongSingers.FirstOrDefault(s => s.SingerId == singerId);
        //    if (singer != null)
        //    {
        //        if (Song.SongSingers.Any(s => s.SingerId == singerId))
        //            Song.SongSingers.Remove(singer);
        //        else
        //            Song.SongSingers.Add(singer);
        //    }
        //}

        public IActionResult OnPostAddSinger()
        {
            string value = Request.Form["submit"];
            if (value.Equals("Bỏ chọn"))
            {
                CreateSongRequestModel.SongSingers.Remove(CreateSongRequestModel.SongSingers.FirstOrDefault(x => x.SingerId == SelectedSingerId));
            }
            else
            {
                CreateSongRequestModel.SongSingers.Add(new SongSingerRequestModel() { SingerId = SelectedSingerId });

                SearchSingerResults.RemoveAll(a => a.SingerId == SelectedSingerId);
            }
            return Page();
            //return Partial("_SearchSingerResults", this);
        }

        public void OnPostAddGerne()
        {
            string value = Request.Form["submit"];
            if (value.Equals("Bỏ chọn"))
            {
                CreateSongRequestModel.SongGenres.Remove(CreateSongRequestModel.SongGenres.FirstOrDefault(x => x.GenreId == SelectedGenreId));
            }
            else
            {
                CreateSongRequestModel.SongGenres.Add(new SongGenreRequestModel() { GenreId = SelectedGenreId });

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
