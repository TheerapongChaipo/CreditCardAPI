using CreditCardAPI.Controllers;
using CreditCardAPI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace CreditCardAPI.Tests.Controllers
{
	[TestFixture]
	public class ValidationControllerTest
	{
		private ValidationController controller;

		[SetUp]
		public void SetupUnitTest()
		{			
		    controller = new ValidationController()
			{
				Request = new HttpRequestMessage()
				{
					Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
				}
			};
		}

		[TearDown]
		public void TearDownUnitTest()
		{
			controller  = null;
		}

		#region BadRequestTest

		[Test]
		public void ValidateCreditCard_BadRequestWithEmpty_CreditCardNumber_ShouldReturn400()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = null,
				StatusCode = 400,
				IsExist = "Does not exist",
				IsValid = "Invalid",
				StatusMessage = "Invalid Null Request: creditcardnumber"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard(null, "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsExist, actualResponse.IsExist);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.StatusMessage, actualResponse.StatusMessage);
		}

		[Test]
		public void ValidateCreditCard_BadRequestWithEmpty_expirydate_ShouldReturn400()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "371449635398431",
				StatusCode = 400,
				IsExist = "Does not exist",
				IsValid = "Invalid",
				StatusMessage = "Invalid Null Request: expirydate"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("371449635398431", "");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsExist, actualResponse.IsExist);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.StatusMessage, actualResponse.StatusMessage);
		}

		[Test]
		public void ValidateCreditCard_BadRequestWithLongCharactor_CreditCardNumber_ShouldReturn400()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				StatusCode = 400,
				StatusMessage = "Invalid - Credit Card Number out of length."
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("11111111111111111", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusMessage, actualResponse.StatusMessage);
		}

		[Test]
		public void ValidateCreditCard_BadRequestWithLongCharactor_expirydate_ShouldReturn400()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				StatusCode = 400,
				StatusMessage = "Invalid - expirydate out of length."
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("3530111333300000", "1211211");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusMessage, actualResponse.StatusMessage);
		}
		
		[Test]
		public void ValidateCreditCard_UNKNOWN_BadRequestCardNumStartWith6_ShouldReturnStatus400_TypeUnknow_Invalid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "671449635398422",
				StatusCode = 400,
				IsValid = "Invalid",
				CardType = "UNKNOWN"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("671449635398422", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.CardType, actualResponse.CardType);
		}

		
		#endregion

		[Test]
		public void ValidateCreditCard_ValidReques_ShouldReturnValidCardandStatus200()
		{
		
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "371449635398431",
				StatusCode = 200,
				IsExist = "exist",
				IsValid = "Invalid",
			};	
	
			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("371449635398431", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);
			
			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsExist, actualResponse.IsExist);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);		
		}


		#region AMEX
		//This card type dont have validation rule whit expiry date , So should Invalid all case
		[Test]
		public void ValidateCreditCard_AMEX_CardNumStartwith34_ShouldReturnStatus200_Invalid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "348734493671000",
				StatusCode = 200,
				IsValid = "Invalid"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("348734493671000", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);			
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);		
		}

		public void ValidateCreditCard_AMEX_CardNumStartwith37_ShouldReturnStatus200_Invalid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "371449635398431",
				StatusCode = 200,
				IsValid = "Invalid"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("348734493671000", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
		}

		#endregion


		#region JCB
		[Test]
		public void ValidateCreditCard_JCB_CardNumStartwith3528_ShouldReturnStatus200_Valid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "3528111333300000",
				StatusCode = 200,
				IsValid = "Valid",
				CardType = "JCB"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("3528111333300000", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.CardType, actualResponse.CardType);
		}

		[Test]
		public void ValidateCreditCard_JCB_CardNumStartwith3589_ShouldReturnStatus200_Valid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "3589111333300000",
				StatusCode = 200,
				IsValid = "Valid",
				CardType = "JCB"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("3589111333300000", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.CardType, actualResponse.CardType);
		}

		[Test]
		public void ValidateCreditCard_JCB_CardNumStartwith3590_ShouldReturnStatus400_Invalid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "3590111333300000",
				StatusCode = 400,
				IsValid = "Invalid",
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("3590111333300000", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
		}
		#endregion

		#region VISA
		[Test]
		public void ValidateCreditCard_VISA_CardNumStartwith4_ShouldReturnStatus200_Valid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "4111111111111111",
				StatusCode = 200,
				IsValid = "Valid",
				CardType = "VISA"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("4111111111111111", "112024");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.CardType, actualResponse.CardType);
		}

		[Test]
		public void ValidateCreditCard_VISA_CardNumStartwith4_leapyear_ShouldReturnStatus200_Valid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "4012888888881881",
				StatusCode = 200,
				IsValid = "Valid",
				CardType = "VISA"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("4012888888881881", "112024");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.CardType, actualResponse.CardType);
		}

		[Test]
		public void ValidateCreditCard_VISA_CardNumStartwith4_Notleapyear_ShouldReturnStatus200_Invalid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "4012888888881881",
				StatusCode = 200,
				IsValid = "Invalid"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("4012888888881881", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
		}
		#endregion

		#region MASTERCARD
		[Test]
		public void ValidateCreditCard_MASTERCARD_CardNumStartwith5_ShouldReturnStatus200_Valid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "5105105105105100",
				StatusCode = 200,
				IsValid = "Valid",
				CardType = "MASTERCARD"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("5105105105105100", "112111");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.CardType, actualResponse.CardType);
		}

		[Test]
		public void ValidateCreditCard_MASTERCARD_CardNumStartwith5_PrimeYear_ShouldReturnStatus200_Valid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "5105105105105100",
				StatusCode = 200,
				IsValid = "Valid",
				CardType = "MASTERCARD"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("5105105105105100", "112111");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.CardType, actualResponse.CardType);
		}

		[Test]
		public void ValidateCreditCard_MASTERCARD_CardNumStartwith5_NotPrimeYear_ShouldReturnStatus200_Valid()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "5105105105105100",
				StatusCode = 200,
				IsValid = "Invalid",
				CardType = "MASTERCARD"
			};

			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation");

			//Act
			var actualResult = controller.ValidateCreditCard("5105105105105100", "112018");
			var objContent = actualResult.Content as ObjectContent;
			Assert.NotNull(objContent);
			var actualResponse = objContent.Value as ValidateCreditCardResponse;
			Assert.NotNull(actualResponse);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.AreEqual(expectedModelResponse.StatusCode, actualResponse.StatusCode);
			Assert.AreEqual(expectedModelResponse.CreditCardNumber, actualResponse.CreditCardNumber);
			Assert.AreEqual(expectedModelResponse.IsValid, actualResponse.IsValid);
			Assert.AreEqual(expectedModelResponse.CardType, actualResponse.CardType);
		}
		#endregion
	}
}
