using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Transactions.Details
{
    public class GameModel : PageModel
    {
        private readonly ILogger<GameModel> logger;
        private readonly IApiClient apiClient;

        public GameModel(ILogger<GameModel> logger, IApiClient apiClient)
        {
            this.logger = logger;
            this.apiClient = apiClient;
        }

        [BindProperty]
        public DTOModels.Response.InAppTransaction InAppTransaction { get; set; } = default!;

        public async Task<IActionResult> OnGet(string id)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.GameTransactionResouce;

            var response = await apiClient.GetAsync(uri + "?InAppTransactionId=" + id);

            var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            var game = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.InAppTransaction>>(jsonResponse).Results.First();
#pragma warning restore CS8601 // Possible null reference assignment.



            if (id == null || game == null)
            {
                return NotFound();
            }

            if (game == null)
            {
                return NotFound();
            }
            else
            {
                //game.Status = (new InAppTransactionStatuses()).List[(int)Enum.Parse(typeof(InAppTransactionStatus), game.Status)];
                //game.TransactionType = (new InAppTransactionTypes()).List[(int)Enum.Parse(typeof(InAppTransactionType), game.TransactionType) - 1];
                game.CreatedDate = game.CreatedDate.Value.AddHours(7);
                InAppTransaction = game;
            }

            return Page();
        }
    }
}
