using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Data.Contract;
using Shared.Data.Model;
using Shared.Utils;

namespace Frontend.Controllers
{
	public class BpkbController : Controller
	{
        private readonly IConfiguration _configuration;
        public BpkbController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> CreateAsync()
        {
            //List<ms_storage_location> cl = new List<ms_storage_location>();
            List<SelectListItem> cl = new List<SelectListItem>();
            SelectListItem temp = new SelectListItem();
            temp.Text = "Pilih Lokasi Penyimpanan";
            temp.Value = "0";
            cl.Add(temp);
            try
            {
                string BaseUri = _configuration.GetSection("Uri").GetSection("BaseUri").Value.ToString();
                string GetTokenPath = _configuration.GetSection("Uri").GetSection("GetToken").Value.ToString();
                string GetLocation = _configuration.GetSection("Uri").GetSection("GetLocation").Value.ToString();
                string Key = _configuration.GetSection("JwtAccess").GetSection("Key").Value.ToString();
                string Value = _configuration.GetSection("JwtAccess").GetSection("Value").Value.ToString();

                string FinalUri = BaseUri + GetTokenPath;
                var response = await new Helpers().RequestToken(FinalUri, Key, Value);
                if (!string.IsNullOrEmpty(response.token))
                {
                    FinalUri = BaseUri + GetLocation;
                    BaseRequest<Abstract> payload = new BaseRequest<Abstract>();
                    var respGetLocation = await new Helpers.HTTPService().PostWithTokenResultValue<Abstract,List<ms_storage_location>>(FinalUri, response.token, payload);
                    if (respGetLocation.Item2.Value!=null)
                    {
                        cl.AddRange(respGetLocation.Item2.Value.Select(x=> new SelectListItem { Text=x.location_name,Value= x.location_id }).ToList());
                    }
                }
            }
            catch (Exception e)
            {
            }

            ViewBag.LocationList = cl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InsertDataAsync(InsertData insertData)
        {
            try
            {
                string BaseUri = _configuration.GetSection("Uri").GetSection("BaseUri").Value.ToString();
                string GetTokenPath = _configuration.GetSection("Uri").GetSection("GetToken").Value.ToString();
                string InsertBpkbPath = _configuration.GetSection("Uri").GetSection("InsertData").Value.ToString();
                string Key = _configuration.GetSection("JwtAccess").GetSection("Key").Value.ToString();
                string Value = _configuration.GetSection("JwtAccess").GetSection("Value").Value.ToString();

                string FinalUri = BaseUri + GetTokenPath;
                var response = await new Helpers().RequestToken(FinalUri, Key, Value);
                if (!string.IsNullOrEmpty(response.token))
                {
                    FinalUri = BaseUri + InsertBpkbPath;
                    InsertData data = new InsertData();
                    BaseRequest<InsertData> payload = new BaseRequest<InsertData>();
                    string UserID = HttpContext.Session.GetString("UserID") ?? "";
                    if (!string.IsNullOrEmpty(UserID))
                    {
                        insertData.user_id = Convert.ToInt64(HttpContext.Session.GetString("UserID"));
                        payload.Value = insertData;

                        var respInsert = await new Helpers.HTTPService().PostWithTokenResultValue<InsertData, ResponseInsert>(FinalUri, response.token, payload);
                        if (!respInsert.Item2.ErrorStatus)
                        {
                            return Json(new { status = true, message = "Insert Data Successfull!" });
                        }
                        else
                        {
                            return Json(new { status = true, message = respInsert.Item2.ErrorMessage });
                        }
                    }
                    else
                    {
                        return Json(new { status = false, message = "Upss, Something wrong!" });
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

