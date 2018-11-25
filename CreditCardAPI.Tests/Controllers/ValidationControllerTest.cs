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

	/*	[Test]
		public void GetUsersByUuids_ShouldCallUsersManagerGetUsersByQueryStringMethodAtLeastOnce()
		{
			//Arrange
			_mockRepository.Setup(m => m.SearchUsers(It.IsAny<SearchQuery>())).Returns(new SearchResult<DTO.User>());
			var controller = new UsersController(_mockRepository.Object) { Request = new HttpRequestMessage() };
			controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
			controller.Request.RequestUri = new Uri(@"http://localhost:65032/identityapi/v1/users?q=uuid:uuid1,uuid2");

			//Act
			controller.GetUsers("uuid:uuid1,uuid2");

			//Assert
			_mockRepository.Verify(m => m.SearchUsers(It.IsAny<SearchQuery>()), Times.Once());
		}

		[Test]
		public void GetUsersByUuids_NotExist_ShouldThrowHttpStatusOKWithEmptyResult()
		{
			//Arrange
			_mockRepository.Setup(m => m.SearchUsers(It.IsAny<SearchQuery>())).Returns(new SearchResult<DTO.User>
			{
				Count = 0,
				Total = 0,
				Items = new DTO.User[] { }
			});
			var controller = new UsersController(_mockRepository.Object) { Request = new HttpRequestMessage() };
			controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
			controller.Request.RequestUri = new Uri(@"http://localhost:65032/identityapi/v1/users?q=uuid:uuid1,uuid2");

			//Act
			var actualResult = controller.GetUsers("uuid:uuid1,uuid2");
			var result = actualResult.Content.ReadAsStringAsync().Result;
			dynamic jsonResult = JsonConvert.DeserializeObject(result);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
			Assert.IsEmpty(jsonResult["users"]);
		}

		[Test]
		public void GetUsersByUuids_ErrorInManager_ShouldThrowHttpStatusInternalServerError()
		{
			//Arrange
			var controller = new UsersController(_mockRepository.Object) { Request = new HttpRequestMessage() };
			controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
			controller.Request.RequestUri = new Uri(@"http://localhost:65032/identityapi/v1/users?q=uuid:uuid1,uuid2");

			//Act
			var actualResult = controller.GetUsers("uuid:uuid1,uuid2");
			var result = actualResult.Content.ReadAsStringAsync().Result;
			dynamic jsonResult = JsonConvert.DeserializeObject(result);

			//Assert
			Assert.AreEqual(HttpStatusCode.InternalServerError, actualResult.StatusCode);
			Assert.IsNotNull(jsonResult["transactionId"]);
			Assert.AreEqual("500.AAA00001", jsonResult["errorCode"].ToString());
		}

		[Test]
		public void GetUsersByUuids_NoColon_InvalidInputShouldReturnHttpStatusBadRequest()
		{
			//Arrange
			var controller = new UsersController(_mockRepository.Object) { Request = new HttpRequestMessage() };
			controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

			//Act
			var actualResult = controller.GetUsers("abcdefghi123456789");
			var result = actualResult.Content.ReadAsStringAsync().Result;
			dynamic jsonResult = JsonConvert.DeserializeObject(result);

			//Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
			Assert.IsNotNull(jsonResult["transactionId"]);
			Assert.AreEqual("400.AAA00006", jsonResult["errorCode"].ToString());
		}

		[Test]
		public void GetUsersByUuids_KeyDoesNotMatch_InvalidInputShouldReturnHttpStatusBadRequest()
		{
			//Arrange
			var controller = new UsersController(_mockRepository.Object) { Request = new HttpRequestMessage() };
			controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

			//Act
			var actualResult = controller.GetUsers("abc:uuid1,uuid2");
			var result = actualResult.Content.ReadAsStringAsync().Result;
			dynamic jsonResult = JsonConvert.DeserializeObject(result);

			//Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
			Assert.IsNotNull(jsonResult["transactionId"]);
			Assert.AreEqual("400.AAA00009", jsonResult["errorCode"].ToString());
		}
		*/

	}
}
