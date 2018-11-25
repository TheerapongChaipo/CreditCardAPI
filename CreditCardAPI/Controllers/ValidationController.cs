using CreditCardAPI.Helpers;
using CreditCardAPI.Models;
using Swashbuckle.Swagger.Annotations;
using System;
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
		[SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(ValidateCreditCardResponse))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ValidateCreditCardResponse))]
		public HttpResponseMessage ValidateCreditCard(string creditcardnumber, string expirydate)
		{
			MethodBase currentMethod = MethodBase.GetCurrentMethod();
			CreditCardLogManager.Entering(string.Format("Card Number : {0}, Expiry Date : {1}", creditcardnumber, expirydate), currentClass, currentMethod);

			return Request.CreateResponse(HttpStatusCode.InternalServerError, new ValidateCreditCardResponse() {
				CreditCardNumber = creditcardnumber,
				StatusCode = HttpStatusCode.InternalServerError.ToString(),
				IsExist = true,
				IsValid = true,
				CardType = "MasterCard"
			});
			
		}
	}
}
