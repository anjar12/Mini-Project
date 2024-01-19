using System;
using Shared.Data.Contract;
using Shared.Data.Model;

namespace Shared.Interfaces
{
	public interface IMasterRepositories
	{
        Task<Tuple<bool, BaseResponse, BaseResponseValue<ResponseLogin>>> Login(BaseRequest<RequestAuth> postData);
        Task<Tuple<bool, BaseResponse, BaseResponseValue<List<ms_storage_location>>>> GetLocation();
    }
}

