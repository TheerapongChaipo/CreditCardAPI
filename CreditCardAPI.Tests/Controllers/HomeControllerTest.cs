using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreditCardAPI;
using CreditCardAPI.Controllers;
using NUnit.Framework;

namespace CreditCardAPI.Tests.Controllers
{
	[TestFixture]
	public class HomeControllerTest
	{
		[Test]
		public void HomeControllerIndexMethodShouldRedirectToSwaggerPage()
		{
			var homeContoller = new HomeController();
			var result = homeContoller.Index() as RedirectResult;
			NUnit.Framework.Assert.IsNotNull(result);
			NUnit.Framework.Assert.AreEqual(result.Url, "~/swagger");
		}
	}
}
