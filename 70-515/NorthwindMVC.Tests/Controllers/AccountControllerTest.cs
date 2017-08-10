using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindMVC;
using NorthwindMVC.Controllers;
using NorthwindMVC.Models;

namespace NorthwindMVC.Tests.Controllers
{

	[TestClass]
	public class AccountControllerTest
	{

		[TestMethod]
		public void ChangePassword_Get_ReturnsView()
		{
			// Arrange
			AccountController controller = GetAccountController();

			// Act
			ActionResult result = controller.Manage((LocalPasswordModel) null);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(10, ((ViewResult)result).ViewData["PasswordLength"]);
		}

		[TestMethod]
		public void ChangePassword_Post_ReturnsRedirectOnSuccess()
		{
			// Arrange
			AccountController controller = GetAccountController();
			LocalPasswordModel model = new LocalPasswordModel()
			{
				OldPassword = "goodOldPassword",
				NewPassword = "goodNewPassword",
				ConfirmPassword = "goodNewPassword"
			};

			// Act
			ActionResult result = controller.Manage(model);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
			Assert.AreEqual("ChangePasswordSuccess", redirectResult.RouteValues["action"]);
		}

		[TestMethod]
		public void ChangePassword_Post_ReturnsViewIfChangePasswordFails()
		{
			// Arrange
			AccountController controller = GetAccountController();
			LocalPasswordModel model = new LocalPasswordModel()
			{
				OldPassword = "goodOldPassword",
				NewPassword = "badNewPassword",
				ConfirmPassword = "badNewPassword"
			};

			// Act
			ActionResult result = controller.Manage(model);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			ViewResult viewResult = (ViewResult)result;
			Assert.AreEqual(model, viewResult.ViewData.Model);
			Assert.AreEqual("The current password is incorrect or the new password is invalid.", controller.ModelState[""].Errors[0].ErrorMessage);
			Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
		}

		[TestMethod]
		public void ChangePassword_Post_ReturnsViewIfModelStateIsInvalid()
		{
			// Arrange
			AccountController controller = GetAccountController();
			LocalPasswordModel model = new LocalPasswordModel()
			{
				OldPassword = "goodOldPassword",
				NewPassword = "goodNewPassword",
				ConfirmPassword = "goodNewPassword"
			};
			controller.ModelState.AddModelError("", "Dummy error message.");

			// Act
			ActionResult result = controller.Manage(model);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			ViewResult viewResult = (ViewResult)result;
			Assert.AreEqual(model, viewResult.ViewData.Model);
			Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
		}

		[TestMethod]
		public void ChangePasswordSuccess_ReturnsView()
		{
			// Arrange
			AccountController controller = GetAccountController();

			// Act
			ActionResult result = controller.Manage(new LocalPasswordModel() {});

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void LogOff_LogsOutAndRedirects()
		{
			// Arrange
			AccountController controller = GetAccountController();

			// Act
			ActionResult result = controller.LogOff();

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
			Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
			Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
		}

		[TestMethod]
		public void LogOn_Get_ReturnsView()
		{
			// Arrange
			AccountController controller = GetAccountController();

			// Act
			ActionResult result = controller.Login(null);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void LogOn_Post_ReturnsRedirectOnSuccess_WithoutReturnUrl()
		{
			// Arrange
			AccountController controller = GetAccountController();
			LoginModel model = new LoginModel()
			{
				UserName = "someUser",
				Password = "goodPassword",
				RememberMe = false
			};

			// Act
			ActionResult result = controller.Login(model, null);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
			Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
			Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
		}

		[TestMethod]
		public void LogOn_Post_ReturnsRedirectOnSuccess_WithReturnUrl()
		{
			// Arrange
			AccountController controller = GetAccountController();
			LoginModel model = new LoginModel()
			{
				UserName = "someUser",
				Password = "goodPassword",
				RememberMe = false
			};

			// Act
			ActionResult result = controller.Login(model, "/someUrl");

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectResult));
			RedirectResult redirectResult = (RedirectResult)result;
			Assert.AreEqual("/someUrl", redirectResult.Url);
		}

		[TestMethod]
		public void LogOn_Post_ReturnsViewIfModelStateIsInvalid()
		{
			// Arrange
			AccountController controller = GetAccountController();
			LoginModel model = new LoginModel()
			{
				UserName = "someUser",
				Password = "goodPassword",
				RememberMe = false
			};
			controller.ModelState.AddModelError("", "Dummy error message.");

			// Act
			ActionResult result = controller.Login(model, null);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			ViewResult viewResult = (ViewResult)result;
			Assert.AreEqual(model, viewResult.ViewData.Model);
		}

		[TestMethod]
		public void LogOn_Post_ReturnsViewIfValidateUserFails()
		{
			// Arrange
			AccountController controller = GetAccountController();
			LoginModel model = new LoginModel()
			{
				UserName = "someUser",
				Password = "badPassword",
				RememberMe = false
			};

			// Act
			ActionResult result = controller.Login(model, null);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			ViewResult viewResult = (ViewResult)result;
			Assert.AreEqual(model, viewResult.ViewData.Model);
			Assert.AreEqual("The user name or password provided is incorrect.", controller.ModelState[""].Errors[0].ErrorMessage);
		}

		[TestMethod]
		public void Register_Get_ReturnsView()
		{
			// Arrange
			AccountController controller = GetAccountController();

			// Act
			ActionResult result = controller.Register();

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(10, ((ViewResult)result).ViewData["PasswordLength"]);
		}

		[TestMethod]
		public void Register_Post_ReturnsRedirectOnSuccess()
		{
			// Arrange
			AccountController controller = GetAccountController();
			RegisterModel model = new RegisterModel()
			{
				UserName = "someUser",
				Password = "goodPassword",
				ConfirmPassword = "goodPassword"
			};

			// Act
			ActionResult result = controller.Register(model);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
			Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
			Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
		}

		[TestMethod]
		public void Register_Post_ReturnsViewIfRegistrationFails()
		{
			// Arrange
			AccountController controller = GetAccountController();
			RegisterModel model = new RegisterModel()
			{
				UserName = "duplicateUser",
				Password = "goodPassword",
				ConfirmPassword = "goodPassword"
			};

			// Act
			ActionResult result = controller.Register(model);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			ViewResult viewResult = (ViewResult)result;
			Assert.AreEqual(model, viewResult.ViewData.Model);
			Assert.AreEqual("Username already exists. Please enter a different user name.", controller.ModelState[""].Errors[0].ErrorMessage);
			Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
		}

		[TestMethod]
		public void Register_Post_ReturnsViewIfModelStateIsInvalid()
		{
			// Arrange
			AccountController controller = GetAccountController();
			RegisterModel model = new RegisterModel()
			{
				UserName = "someUser",
				Password = "goodPassword",
				ConfirmPassword = "goodPassword"
			};
			controller.ModelState.AddModelError("", "Dummy error message.");

			// Act
			ActionResult result = controller.Register(model);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			ViewResult viewResult = (ViewResult)result;
			Assert.AreEqual(model, viewResult.ViewData.Model);
			Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
		}

		private static AccountController GetAccountController()
		{
			AccountController controller = new AccountController();

			controller.ControllerContext = new ControllerContext()
			{
				Controller = controller,
				RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
			};

			return controller;
		}

		private class MockHttpContext : HttpContextBase
		{
			private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("someUser"), null /* roles */);

			public override IPrincipal User
			{
				get
				{
					return _user;
				}
				set
				{
					base.User = value;
				}
			}
		}
	}
}
