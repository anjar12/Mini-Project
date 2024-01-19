using System;
using Shared.Data.Model;

namespace Shared.Interfaces
{
	public interface ITransactionRepositories
	{
        Task<Tuple<bool, BaseResponse,BaseResponseValue<ResponseInsert>>> InsertDataBpkb(BaseRequest<InsertData> postData);
    }
}

