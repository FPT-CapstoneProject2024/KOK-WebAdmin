using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Configuration;
using WebAdmin.Context;
using WebAdmin.DTOModels.Request.Item;
using WebAdmin.DTOModels.Request.Singer;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Singer
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
        public DTOModels.Request.Singer.SingerRequestModel UpdateSinger { get; set; } = default!;
        [BindProperty]
        public DTOModels.Response.Singer Singer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SingerResource + "?SingerId=" + id;
            var response = await _apiClient.GetAsync(uri);
            var responeJson = await response.Content.ReadAsStringAsync();
            var singer = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Singer>>(responeJson);

            if (singer.Results == null)
            {
                return NotFound();
            }
            Singer = singer.Results.First();
            UpdateSinger = _mapper.Map<SingerRequestModel>(Singer);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
            //if (!ModelState.IsValid)
            //{
            //    return new JsonResult(new { success = false });
            //}
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

                UpdateSinger.Image = imageUrl;
            }
            else
            {
                UpdateSinger.Image = Singer.Image;
            }

            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SingerResource + "/" + Singer.SingerId;
                var response = await _apiClient.PutAsync(uri, UpdateSinger);
                var responeJson = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Singer>>(responeJson);

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
