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
				IsExist = false,
				IsValid = false,
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
				IsExist = false,
				IsValid = false,
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

		#endregion
		
		[Test]
		public void ValidateCreditCard_ValidRequest_ShouldReturnValidCardandStatus200()
		{
			//Arrange	
			var expectedModelResponse = new ValidateCreditCardResponse
			{
				CreditCardNumber = "371449635398431",
				StatusCode = 200,
				IsExist = true,
				IsValid = true,
			};	
	
			controller.Request.RequestUri = new Uri(@"http://localhost:60855/api/Validation?creditcardnumber=371449635398431&expirydate=112018");

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
		
	}
}
