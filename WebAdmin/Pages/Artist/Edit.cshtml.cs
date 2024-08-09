using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Request.Artist;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Artist
{
    public class EditModel : PageModel
    {
        private readonly IApiClient _apiClient;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        private static string? imageUrl { get; set; }   

        public EditModel(IApiClient apiClient, IMapper mapper, IHttpClientFactory clientFactory)
        {
            _apiClient = apiClient;
            _mapper = mapper;
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public DTOModels.Request.Artist.ArtistRequestModel UpdateArtist { get; set; } = default!;
        [BindProperty]
        public DTOModels.Response.Artist Artist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ArtistResource + "?ArtistId=" + id;
            var response = await _apiClient.GetAsync(uri);
            var responeJson = await response.Content.ReadAsStringAsync();
            var singer = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Artist>>(responeJson);

            if (singer.Results == null)
            {
                return NotFound();
            }
            Artist = singer.Results.First();
            UpdateArtist = _mapper.Map<ArtistRequestModel>(Artist);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dataImage = await SupportingFeature.Instance.UploadImage(_clientFactory, file, KokApiContext.ImgurClientId);

            if (!dataImage.Item1)
            {
                ViewData["Message"] = dataImage.Item2;
                return Page();
            }
            else
            {
                ViewData["ImageUrl"] = dataImage.Item2;
                imageUrl = dataImage.Item2;
            }

            UpdateArtist.Image = imageUrl;


            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ArtistResource + "/" + Artist.ArtistId;
                var response = await _apiClient.PutAsync(uri, UpdateArtist);
                var responeJson = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Artist>>(responeJson);

                if (item.result.Value == false)
                {
                    return Page();
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
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }

        //private bool ItemExists(Guid id)
        //{
        //    return (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
        //}
    }
}
