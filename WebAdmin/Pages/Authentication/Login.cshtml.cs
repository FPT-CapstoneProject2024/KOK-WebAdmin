﻿using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WebAdmin.Context;
using WebAdmin.DTOModels;
using WebAdmin.DTOModels.Response.Authentication;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private const string JwtAccessToken = "jwtAccessToken";
        //public static Guid? AccountId { get; set; } 

        public class LoginCredential
        {
            [EmailAddress]
            [Required]
            public string Email { get; set; } = string.Empty;
            [Required]
            public string Password { get; set; } = string.Empty;

            
        }

        [BindProperty]
        public LoginCredential Credential { get; set; } = new LoginCredential();

        private readonly ILogger<LoginModel> logger;
        private readonly IApiClient apiClient;

        public LoginModel(ILogger<LoginModel> logger, IApiClient apiClient)
        {
            this.logger = logger;
            this.apiClient = apiClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AuthenticationResource + "/login";
            var response = await apiClient.PostAsync(uri, Credential);

            // Success
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var loginResult = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                if (loginResult != null && loginResult.Value != null)
                {
                    if (IsUserAllowed((AccountRole)Enum.Parse(typeof(AccountRole),loginResult.Value.Role)!))
                    {
                        // User is admin or staff, access granted
                        // Store jwt token in cookie
                        Response.Cookies.Append(JwtAccessToken, loginResult.AccessToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                        });

                        Response.Cookies.Append("AccountData", JsonConvert.SerializeObject(loginResult.Value), new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                        });
                        //AccountId = loginResult.Value.AccountId;//

                        //HttpContext.Session.SetString("AccountId", String.Concat(loginResult.Value.AccountId));
                        HttpContext.Session.SetString("AccountData", JsonConvert.SerializeObject(loginResult.Value));
                        //HttpContext.Session.SetString("AccessToken", loginResult.AccessToken);


                        // Redirect to home page
                        return RedirectToPage("/Dashboard/Index");

                    }
                    else
                    {
                        // User is not admin or staff, do not allow user access
                        return Page();
                    }
                }
                else
                {

                    ViewData["Message"] = "Vui lòng đăng nhập lại!";
                    logger.LogError("Login success but result is null!");
                    return Page();
                }
            }
            else
            {
                ViewData["Message"] = "Đăng Nhập thất bại!";
                // Incorrect email or password
                logger.LogError("Failed to login");
                //return Page();
                return Page();

            }

            return Page();
        }

        private bool IsUserAllowed(AccountRole accountRole)
        {
            return accountRole == AccountRole.ADMIN || accountRole == AccountRole.STAFF;
        }
    }
}
