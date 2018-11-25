using CreditCardAPI.DataAccess;
using CreditCardAPI.Helpers;
using CreditCardAPI.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace CreditCardAPI.Controllers
{
	public class ValidationController : ApiController
	{
		Type currentClass = typeof(ValidationController);

		/// <summary>
		/// Validate credit card by credit card number and expiry date 
		/// </summary>
		/// <param name="creditcardnumber">Credit Card Number</param>
		/// <param name="expirydate">Expiry date (MMYYYY)</param>
		/// <returns>Valid credit card and credit card type</returns>
		/// <remarks>Returns Valid credit card and credit card type </remarks> 		
		[HttpGet]
		[ActionName("ValidateCreditCard")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ValidateCreditCardResponse))]
		[SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidateCreditCardResponse))]
		//[SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(ValidateCreditCardResponse))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ValidateCreditCardResponse))]
		public HttpResponseMessage ValidateCreditCard(string creditcardnumber, string expirydate)
		{
			MethodBase currentMethod = MethodBase.GetCurrentMethod();
			CreditCardLogManager.Entering(string.Format("Card Number : {0}, Expiry Date : {1}", creditcardnumber, expirydate), currentClass, currentMethod);

			var errorMsg = RequestValidation.BadRequestValidationCardNumber(creditcardnumber, expirydate);
			if (!string.IsNullOrEmpty(errorMsg))
			{
				return BuildResponse(creditcardnumber, HttpStatusCode.BadRequest, (int)HttpStatusCode.BadRequest, errorMsg);
			}

			try
			{
				var response = new ValidateCreditCardResponse();

				using (var context = new CreditcardDBEntities())
				{
					var result = context.ValidateCreditCard(creditcardnumber, expirydate).ToList();
					if (result.Count > 0)
					{
						foreach (var item in result)
						{
							response.CreditCardNumber = item.CARDNUMBER;
							response.StatusCode = (int)HttpStatusCode.OK;
							response.StatusMessage = HttpStatusCode.OK.ToString();
							response.IsExist = item.ISEXIST;
							response.IsValid = item.ISVALID;
							response.CardType = item.CARDTYPE;
						};
					}
				}

				return Request.CreateResponse(HttpStatusCode.OK, response);

			}
			catch (Exception ex)
			{

				return BuildResponse(creditcardnumber, HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError, ex.Message);
			}

		}
		private HttpResponseMessage BuildResponse(string strCardNumber, HttpStatusCode statusCode, int httpCode, string message, string IsExist = "Does not exist", string IsValid = "Invalid", string CardType = "UNKNOWN")
		{
			return Request.CreateResponse(statusCode, new ValidateCreditCardResponse()
			{
				CreditCardNumber = strCardNumber,
				StatusCode = httpCode,
				StatusMessage = message,
				IsExist = IsExist,
				IsValid = IsValid,
				CardType = CardType
			});
		}

	}
}
