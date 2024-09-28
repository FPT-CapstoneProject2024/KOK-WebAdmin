using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Request.Genre;
using WebAdmin.DTOModels.Request.Singer;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Genre
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
        public DTOModels.Request.Genre.GenreRequestModel UpdateGenre { get; set; } = default!;
        [BindProperty]
        public DTOModels.Response.Genre Genre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.GenreResource + "?GenreId=" + id;
            var response = await _apiClient.GetAsync(uri);
            var responeJson = await response.Content.ReadAsStringAsync();
            var gerne = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Genre>>(responeJson);

            if (gerne.Results == null)
            {
                return NotFound();
            }
            Genre = gerne.Results.First();
            UpdateGenre = _mapper.Map<GenreRequestModel>(Genre);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false });
            }

            if (file != null)
            {
                var dataImage = await SupportingFeature.Instance.UploadImage(_clientFactory, file, KokApiContext.ImgurClientId);

                if (!dataImage.Item1)
                {
                    ViewData["Message"] = dataImage.Item2;
                    return new JsonResult(new { success = false });
                }
                else
                {
                    ViewData["ImageUrl"] = dataImage.Item2;
                    imageUrl = dataImage.Item2;
                }

                // Update the image URL in the UpdateGenre model
                UpdateGenre.Image = imageUrl;
            }
            else
            {
                // If no file is uploaded, keep the existing image
                UpdateGenre.Image = Genre.Image;
            }
            //var dataImage = await SupportingFeature.Instance.UploadImage(_clientFactory, file, KokApiContext.ImgurClientId);

            //if (!dataImage.Item1)
            //{
            //    ViewData["Message"] = dataImage.Item2;
            //    return new JsonResult(new { success = false });
            //}
            //else
            //{
            //    ViewData["ImageUrl"] = dataImage.Item2;
            //    imageUrl = dataImage.Item2;
            //}

            //UpdateGenre.Image = imageUrl;

            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.GenreResource + "/" + Genre.GenreId;
                var response = await _apiClient.PutAsync(uri, UpdateGenre);
                var responeJson = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Genre>>(responeJson);

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
    }
}
