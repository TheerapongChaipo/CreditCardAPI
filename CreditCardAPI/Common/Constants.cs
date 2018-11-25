using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardAPI.Common
{
	public class Constants
	{
		public const string InvalidRequest_creditcardNumber = "Invalid Null Request: creditcardnumber";
		public const string InvalidRequest_expirydate = "Invalid Null Request: expirydate";
		public const string InvalidRequest_creditcardNumberOutofLength = "Invalid - Credit Card Number out of length.";
		public const string InvalidRequest_UnknownCreditcard = "Unknown Credit Card Number";
		public const string InvalidRequest_expirydateOutofLength = "Invalid - expirydate out of length.";
		public const string InvalidRequest_UnknownExpirydate = "Invalid - expirydate incorrect.";

	}
}