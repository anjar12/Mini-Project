using System;
using Microsoft.AspNetCore.Mvc;
using Shared.Data.Model;
using Shared.Interfaces;

namespace Service_Transaction.Controllers
{
	public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWorks _unitOfWork;

        public TransactionController(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("InsertData")]
        public async Task<ActionResult> InsertDataBpkb([FromBody] BaseRequest<InsertData> postData)
        {
            var response = await _unitOfWork.transaction.InsertDataBpkb(postData);
            if (response.Item1)
            {
                BaseResponseValue<ResponseInsert> resultValue = response.Item3;
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

