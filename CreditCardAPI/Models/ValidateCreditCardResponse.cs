using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardAPI.Models
{
	/// <summary>
	/// ValidateCreditCardResponse
	/// </summary>
	public class ValidateCreditCardResponse
	{

		/// <summary>
		/// credit card number requestor
		/// </summary>
		[JsonProperty("CreditCardNumber")]
		public string CreditCardNumber { get; set; }

		/// <summary>
		/// Status Code of response 
		/// </summary>
		[JsonProperty("StatusCode")]
		public int StatusCode { get; set; }

		/// <summary>
		/// Status message of response 
		/// </summary>
		[JsonProperty("StatusMessage")]
		public string StatusMessage { get; set; }

		/// <summary>
		/// Card Valid
		/// </summary>
		[JsonProperty("IsValid")]
		public bool IsValid { get; set; }

		/// <summary>
		/// Card Exist
		/// </summary>
		[JsonProperty("IsExist")]
		public bool IsExist { get; set; }

		/// <summary>
		/// Credit Card Type
		/// </summary>
		[JsonProperty("CardType")]
		public string CardType { get; set; }
	}
}