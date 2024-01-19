using System;
namespace Shared.Data.Model
{
	public class AuthToken
	{
        public string? Token { get; set; } = "";
        public DateTime ExpirationDate { get; set; }
    }
}

