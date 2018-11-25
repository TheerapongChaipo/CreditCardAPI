using CreditCardAPI.Common;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CreditCardAPI.Helpers
{
	public class RequestValidation
	{

		public static string BadRequestValidationCardNumber(string creditcardnumber, string expirydate)
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
			if (creditcardnumber.Length >= 15 && creditcardnumber.Length <= 16)
			{
				if (!Regex.IsMatch(creditcardnumber, pattern))
				{
					CreditCardLogManager.Error(Constants.InvalidRequest_UnknownCreditcard, currentClass, currentMethod);
					return Constants.InvalidRequest_UnknownCreditcard;
				}
			}
			else
			{
				CreditCardLogManager.Error(Constants.InvalidRequest_creditcardNumberOutofLength, currentClass, currentMethod);
				return Constants.InvalidRequest_creditcardNumberOutofLength;
			}


			//check expirydate Pattern			
			string expirydatePattern = @"^((0[1-9])|(1[0-2]))(\d{4})$";
			if (expirydate.Length == 6)
			{
				if (!Regex.IsMatch(expirydate, expirydatePattern))
				{
					CreditCardLogManager.Error(Constants.InvalidRequest_UnknownExpirydate, currentClass, currentMethod);
					return Constants.InvalidRequest_UnknownExpirydate;
				}
			}
			else
			{
				CreditCardLogManager.Error(Constants.InvalidRequest_expirydateOutofLength, currentClass, currentMethod);
				return Constants.InvalidRequest_expirydateOutofLength;
			}


			return message;
		}
	}
}