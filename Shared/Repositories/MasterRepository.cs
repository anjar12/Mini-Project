using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Data.Context;
using Shared.Data.Contract;
using Shared.Data.Model;
using Shared.Interfaces;

namespace Shared.Repositories
{
    public class MasterRepository : IMasterRepositories
    {
        private readonly IConfiguration _configuration;
        private readonly ActuatorContext _context;
        public MasterRepository(ActuatorContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }


        public async Task<Tuple<bool, BaseResponse, BaseResponseValue<ResponseLogin>>> Login(BaseRequest<RequestAuth> postData)
        {
            BaseResponseValue<ResponseLogin> resultValue = new BaseResponseValue<ResponseLogin>();
            BaseResponse result = new BaseResponse();
            int errorCode = 1001001;
            try
            {
                if (postData != null)
                {
                    if (postData.Value != null)
                    {
                        var cekDataUsers = _context.ms_user
                                                 .Where(x => x.user_name == postData.Value.username && x.password == postData.Value.password)
                                                 .Select(x => new ResponseLogin {
                                                                                    user_name = x.user_name,
                                                                                    user_id = x.user_id,
                                                                                    is_active = x.is_active })
                                                 .FirstOrDefault() ?? new ResponseLogin();
                        if (cekDataUsers.user_id > 0)
                        {
                            resultValue.Value = cekDataUsers;
                            result = new ResponseError().result(0, false, "");
                            return new Tuple<bool, BaseResponse, BaseResponseValue<ResponseLogin>>(true, result, resultValue);
                        }
                        else
                        {
                            result = new ResponseError().result(errorCode, true, "Data user tidak ditemukan");
                            return new Tuple<bool, BaseResponse, BaseResponseValue<ResponseLogin>>(false, result, resultValue);
                        }
                    }
                    else
                    {
                        result = new ResponseError().result(errorCode, true, "Bad Request");
                        return new Tuple<bool, BaseResponse, BaseResponseValue<ResponseLogin>>(false, result, resultValue);
                    }
                }
                else
                {
                    result = new ResponseError().result(errorCode, true, "Bad Request");
                    return new Tuple<bool, BaseResponse, BaseResponseValue<ResponseLogin>>(false, result, resultValue);
                }
            }
            catch (Exception e)
            {
                result = new ResponseError().result(errorCode, true, e.Message);
                return new Tuple<bool, BaseResponse, BaseResponseValue<ResponseLogin>>(false, result, resultValue);
            }
        }
        public async Task<Tuple<bool, BaseResponse, BaseResponseValue<List<ms_storage_location>>>> GetLocation()
        {
            BaseResponseValue<List<ms_storage_location>> resultValue = new BaseResponseValue<List<ms_storage_location>>();
            BaseResponse result = new BaseResponse();
            int errorCode = 1001002;
            try
            {
                var getData = _context.ms_storage_location.AsNoTracking().ToList() ?? new List<ms_storage_location>();
                if (getData.Any())
                {
                    resultValue.Value = getData;
                    result = new ResponseError().result(0, false, "");
                    return new Tuple<bool, BaseResponse, BaseResponseValue<List<ms_storage_location>>>(true, result, resultValue);
                }
                else
                {
                    result = new ResponseError().result(errorCode, true, "Data user tidak ditemukan");
                    return new Tuple<bool, BaseResponse, BaseResponseValue<List<ms_storage_location>>>(false, result, resultValue);
                }               
            }
            catch (Exception e)
            {
                result = new ResponseError().result(errorCode, true, e.Message);
                return new Tuple<bool, BaseResponse, BaseResponseValue<List<ms_storage_location>>>(false, result, resultValue);
            }
        }
    }
}

