using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Data.Model;
using Shared.Utils;

namespace Frontend.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> ValidateAsync(Shared.Data.Model.RequestAuth admin)
        {
            try
            {
                string BaseUri = _configuration.GetSection("Uri").GetSection("BaseUri").Value.ToString();
                string GetTokenPath = _configuration.GetSection("Uri").GetSection("GetToken").Value.ToString();
                string LoginPath = _configuration.GetSection("Uri").GetSection("Login").Value.ToString();
                string Key = _configuration.GetSection("JwtAccess").GetSection("Key").Value.ToString();
                string Value = _configuration.GetSection("JwtAccess").GetSection("Value").Value.ToString();

                string FinalUri = BaseUri + GetTokenPath;
                var response = await new Helpers().RequestToken(FinalUri, Key, Value);
                if (!string.IsNullOrEmpty(response.token))
                {
                    FinalUri = BaseUri + LoginPath;
                    RequestAuth data = new RequestAuth();
                    BaseRequest<RequestAuth> payload = new BaseRequest<RequestAuth>();

                    data.username = admin.username;
                    data.password = admin.password;
                    payload.Value = data;

                    var respLogin = await new Helpers.HTTPService().PostWithTokenResultValue<RequestAuth,ResponseLogin>(FinalUri, response.token, payload);
                    if (!respLogin.Item2.ErrorStatus)
                    {
                        HttpContext.Session.SetString("UserID", respLogin.Item2.Value.user_id.ToString());
                        return Json(new { status = true, message = "Login Successfull!" });
                    }
                    else
                    {
                        return Json(new { status = true, message = respLogin.Item2.ErrorMessage });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Upss, Something wrong!" });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = false, message = e.Message });
            }
        }
    }
}

