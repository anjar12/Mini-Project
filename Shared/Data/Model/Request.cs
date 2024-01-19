using System;
namespace Shared.Data.Model
{
	public class RequestAuth
	{
		public string username { get; set; }
        public string password { get; set; }
    }
    public class BaseRequest<T>
    {
        public T Value { get; set; }
    }
    public class InsertData
    {
        public string agreement_number { get; set; }

        public string bpkb_no { get; set; }

        public string branch_id { get; set; }

        public string faktur_no { get; set; }

        public string faktur_date { get; set; }

        public string location_id { get; set; }

        public string police_no { get; set; }

        public string bpkb_date_in { get; set; }
        public string bpkb_date{ get; set; }
        public long user_id { get; set; }

    }
    public class HeadersRequest
    {
        public string headerName { get; set; }
        public string headerValue { get; set; }
    }
    public class Abstract
    {
    }
}

