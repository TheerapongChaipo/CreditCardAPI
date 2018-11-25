using CreditCardAPI.Common;
using CreditCardAPI.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CreditCardAPI.Helpers
{
	public class RequestValidation
	{	

		public static string BadRequestValidationCardNumber( string creditcardnumber , string expirydate)
		{
			MethodBase currentMethod = MethodBase.GetCurrentMethod();
			Type currentClass = typeof(RequestValidation);
			var message = string.Empty;
			
			//check empty value
			if (string.IsNullOrEmpty(creditcardnumber))
			{
				const string errorMsg = Constants.InvalidRequest_creditcardNumber;
				CreditCardLogManager.Error(errorMsg, currentClass, currentMethod);
				return errorMsg;
			}	
				
			if (string.IsNullOrEmpty(expirydate))
			{
				const string errorMsg = Constants.InvalidRequest_expirydate;
				CreditCardLogManager.Error(errorMsg, currentClass, currentMethod);				
				return errorMsg;
			}

			//check creditcardnumber  Pattern		
			string pattern = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13}|(?:352[89]|35[3-8][0-9])\d{12})$";
			message = (creditcardnumber.Length < 15 && creditcardnumber.Length > 16) ?
											Constants.InvalidRequest_creditcardNumberOutofLength :
											(!Regex.IsMatch(creditcardnumber, pattern)) ? Constants.InvalidRequest_UnknownCreditcard : string.Empty;
			
			//check expirydate Pattern			
			 string expirydatePattern = @"^((0[1-9])|(1[0-2]))(\d{4})$";
				message = (expirydate.Length != 6) ?
												 Constants.InvalidRequest_expirydateOutofLength :
												(!Regex.IsMatch(expirydate, expirydatePattern)) ? Constants.InvalidRequest_UnknownExpirydate : string.Empty;


			if(!string.IsNullOrEmpty(message))
				CreditCardLogManager.Error(message, currentClass, currentMethod);

			return message;
		}		
	}
}