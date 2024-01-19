using System;
namespace Shared.Data.Model
{
	public class ResponseLogin
	{
		public long user_id { get; set; }
        public string user_name { get; set; }
        public bool is_active { get; set; }
    }
    public class ResponseInsert
    {
        public string agreement_Number { get; set; }
    }
    public class BaseResponseValue<T>
    {
        public int ErrorCode { get; set; }
        public bool ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
        public T Value { get; set; }
    }
    public class BaseResponse
    {
        public int ErrorCode { get; set; }
        public bool ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ResponseError
    {
        public BaseResponse result(int messageCode, bool messageStatus, string messageString)
        {
            BaseResponse result = new BaseResponse();
            result.ErrorCode = messageCode;
            result.ErrorStatus = messageStatus;
            result.ErrorMessage = messageString;
            return result;
        }
    }
    public class HttpResponse
    {
        public string StatusCode { get; set; } = "0000";
        public string ReasonPhrase { get; set; } = "";
    }
    public class JwtToken
    {
        public string token { get; set; }
        public string expirationDate { get; set; }
    }
    public class ApiResponse<T>
    {
        public T Value { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public static ApiResponse<T> Fail(string errorMessage)
        {
            return new ApiResponse<T> { Succeeded = false, Message = errorMessage };
        }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { Succeeded = true, Value = data };
        }
    }
}

