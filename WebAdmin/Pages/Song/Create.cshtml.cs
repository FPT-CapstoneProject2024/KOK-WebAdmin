﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                Song.SongArtists = _mapper.Map<ICollection<SongArtistRequestModel>> (SongIds);
                Song.SongSingers = _mapper.Map<ICollection<SongSingerRequestModel>>(SingerIds);
                Song.SongGenres = _mapper.Map<ICollection<SongGenreRequestModel>>(GenreIds);

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SongResource;

                var response = await apiClient.PostAsync(uri, Song);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Song>>(jsonResponse);

                Song = new CreateSongRequestModel();

                if (data.result.Value)
                {
                    return RedirectToPage("./Index");
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
            return Page();
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

        public IActionResult OnPostAddArtist()
        {

            string value = Request.Form["submit"];
            if (value.Equals("Bỏ chọn"))
            {
                GenreIds.Remove(GenreIds.Find(x => x.GenreId == SelectedArtistId));

                return Page();
            }
            SongIds.Add(new SongArtist() { ArtistId = SelectedArtistId });

            SearchArtistResults.RemoveAll(a => a.ArtistId == SelectedArtistId);
            return Page();
        }
        
        public IActionResult OnPostAddSinger()
        {
            string value = Request.Form["submit"];
            if(value.Equals("Bỏ chọn"))
            {
                SingerIds.Remove(SingerIds.Find(x => x.SingerId == SelectedSingerId));

                return Page();
            }
            SingerIds.Add(new SongSinger() { SingerId = SelectedSingerId });
            return Page();
        }
        
        public IActionResult OnPostAddGerne()
        {
            string value = Request.Form["submit"];
            if (value.Equals("Bỏ chọn"))
            {
                GenreIds.Remove(GenreIds.Find(x => x.GenreId == SelectedGenreId));

                return Page();
            }
            GenreIds.Add(new DTOModels.Response.SongGenre() { GenreId = SelectedGenreId });

            SearchGenreResults.RemoveAll(a => a.GenreId == SelectedGenreId);
            return Page();
        }

    }
}
