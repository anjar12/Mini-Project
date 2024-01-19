using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Data.Contract;
using Shared.Data.Model;
using Shared.Interfaces;

namespace Service_Master.Controllers
{
    [Authorize(AuthenticationSchemes = "access_auth")]
    [ApiController]
    public class MasterController : ControllerBase
	{
        private readonly IUnitOfWorks _unitOfWork;

        public MasterController(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] BaseRequest<RequestAuth> postData)
        {
            var response = await _unitOfWork.master.Login(postData);
            if (response.Item1)
            {
                BaseResponseValue<ResponseLogin> resultValue = response.Item3;
                return Ok(resultValue);
            }
            else
            {
                BaseResponse result = response.Item2;
                return Ok(result);
            }
        }

        [HttpPost("GetLocation")]
        public async Task<ActionResult> GetLocation()
        {
            var response = await _unitOfWork.master.GetLocation();
            if (response.Item1)
            {
                BaseResponseValue<List<ms_storage_location>> resultValue = response.Item3;
                return Ok(resultValue);
            }
            else
            {
                BaseResponse result = response.Item2;
                return Ok(result);
            }
        }
    }
}

