using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using WebAdmin.Context;
using WebAdmin.DTOModels.Request.Song;
using WebAdmin.DTOModels.Response;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Song
{
    public class EditModel : PageModel
    {
        private readonly IApiClient _apiClient;
        private readonly IMapper _mapper;

        public EditModel(IApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        [BindProperty]
        public DTOModels.Response.Song Song { get; set; } = default!;
        [BindProperty]
        public DTOModels.Request.Song.UpdateSongRequestModel UpdateSong { get; set; } = default!;

        public List<DTOModels.Response.Singer> Singers { get; set; }
        public List<DTOModels.Response.Artist> Artists { get; set; }
        public List<DTOModels.Response.Genre> Genres { get; set; }


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SongResource + "?songId=" + id;
                var response = await _apiClient.GetAsync(uri);
                var responeJson = await response.Content.ReadAsStringAsync();
                var song = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Song>>(responeJson);

                if (song.Results == null)
                {
                    return NotFound();
                }
                Song = song.Results.First();

                UpdateSong = _mapper.Map<UpdateSongRequestModel>(Song);


                //uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SingerResource + "?Status=ACTIVE";

                //response = await _apiClient.GetAsync(uri);
                //responeJson = await response.Content.ReadAsStringAsync();
                //Singers = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Singer>>(responeJson).Results;
                
                
                

                
                
                //uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ArtistResource + "?Status=ACTIVE";

                //response = await _apiClient.GetAsync(uri);
                //responeJson = await response.Content.ReadAsStringAsync();
                //Artists = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Artist>>(responeJson).Results;
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
            if (id == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return new JsonResult(new { success = false });
            //}

            try
            {
                //_context.Attach(Item).State = EntityState.Modified;
                UpdateSong.CreatorId = JsonConvert.DeserializeObject<DTOModels.Response.Account>(HttpContext.Request.Cookies["AccountData"])?.AccountId;
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SongResource + "/" + Song.SongId;
                var response = await _apiClient.PutAsync(uri, UpdateSong);
                var responeJson = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Song>>(responeJson);

                if (item.result.Value)
                {
                    return new JsonResult(new { success = true });
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ItemExists(Item.ItemId))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                return new JsonResult(new { success = false });
            }

            return new JsonResult(new { success = false });
        }

        //private bool ItemExists(Guid id)
        //{
        //    return (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
        //}

        public async Task<IActionResult> OnGetSearchGenre(string query)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.GenreResource + "?Status=ACTIVE";

            var response = await _apiClient.GetAsync(uri);
            var responeJson = await response.Content.ReadAsStringAsync();
            Genres = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Genre>>(responeJson).Results;

            return new JsonResult(Genres);


        }
    }
}
