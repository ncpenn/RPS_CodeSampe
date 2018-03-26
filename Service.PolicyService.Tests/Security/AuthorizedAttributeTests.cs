using JWT.Algorithms;
using JWT.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.PolicyService.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Service.PolicyService.Tests.Security
{
    [TestClass]
    public class AuthorizedAttributeTests : AuthenticateAttribute
    {
        public new bool IsAuthorized(HttpActionContext httpActionContext)
        {
            return base.IsAuthorized(httpActionContext);
        }

        [TestMethod]
        public void AuthenticateAttribute_Null_Header_Returns_False()
        {
            //Arrange
            var context = new HttpActionContext();
            var request = new HttpRequestMessage();
            request.Headers.Authorization = null;
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;
            context.ControllerContext = controllerContext;

            //Act
            var isAuthenticated = base.IsAuthorized(context);

            //Assert
            Assert.IsFalse(isAuthenticated);
        }

        [TestMethod]
        public void AuthenticateAttribute_Invalid_Token_Returns_False()
        {
            //Arrange
            var context = new HttpActionContext();
            var request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue("jwt", "hi mom!");
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;
            context.ControllerContext = controllerContext;

            //Act
            var isAuthenticated = base.IsAuthorized(context);

            //Assert
            Assert.IsFalse(isAuthenticated);
        }

        [TestMethod]
        public void AuthenticateAttribute_Incorrectly_Signed_Token_Returns_False()
        {
            //Arrange

            var token = new JwtBuilder()
               .WithAlgorithm(new HMACSHA256Algorithm())
               .WithSecret("not the right secret")
               .AddClaim(ClaimName.ExpirationTime.ToString(), DateTime.UtcNow.AddMinutes(2))
               .AddClaim(ClaimName.NotBefore.ToString(), DateTime.UtcNow.AddSeconds(-2))
               .Build();

            var context = new HttpActionContext();
            var request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue("jwt", token);
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;
            context.ControllerContext = controllerContext;

            //Act
            var isAuthenticated = base.IsAuthorized(context);

            //Assert
            Assert.IsFalse(isAuthenticated);
        }

        [TestMethod]
        public void AuthenticateAttribute_Valid_Token_Returns_True()
        {
            //Arrange

            var token = new JwtBuilder()
               .WithAlgorithm(new HMACSHA256Algorithm())
               .WithSecret(ConfigurationManager.AppSettings["PolicyServiceSecret"])
               .AddClaim(ClaimName.ExpirationTime.ToString(), DateTime.UtcNow.AddMinutes(2))
               .AddClaim(ClaimName.NotBefore.ToString(), DateTime.UtcNow.AddSeconds(-2))
               .Build();

            var context = new HttpActionContext();
            var request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue("jwt", token);
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;
            context.ControllerContext = controllerContext;

            //Act
            var isAuthenticated = base.IsAuthorized(context);

            //Assert
            Assert.IsTrue(isAuthenticated);
        }

        [TestMethod]
        public void AuthenticateAttribute_Missing_Expiry_Claim_Returns_False()
        {
            //Arrange

            var token = new JwtBuilder()
               .WithAlgorithm(new HMACSHA256Algorithm())
               .WithSecret(ConfigurationManager.AppSettings["PolicyServiceSecret"])
               .AddClaim(ClaimName.NotBefore.ToString(), DateTime.UtcNow.AddSeconds(-2))
               .Build();

            var context = new HttpActionContext();
            var request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue("jwt", token);
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;
            context.ControllerContext = controllerContext;

            //Act
            var isAuthenticated = base.IsAuthorized(context);

            //Assert
            Assert.IsFalse(isAuthenticated);
        }

        [TestMethod]
        public void AuthenticateAttribute_Missing_NotBefore_Claim_Returns_False()
        {
            //Arrange

            var token = new JwtBuilder()
               .WithAlgorithm(new HMACSHA256Algorithm())
               .WithSecret(ConfigurationManager.AppSettings["PolicyServiceSecret"])
               .AddClaim(ClaimName.ExpirationTime.ToString(), DateTime.UtcNow.AddMinutes(2))
               .Build();

            var context = new HttpActionContext();
            var request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue("jwt", token);
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;
            context.ControllerContext = controllerContext;

            //Act
            var isAuthenticated = base.IsAuthorized(context);

            //Assert
            Assert.IsFalse(isAuthenticated);
        }

        [TestMethod]
        public void AuthenticateAttribute_Future_NotBefore_Claim_Returns_False()
        {
            //Arrange

            var token = new JwtBuilder()
               .WithAlgorithm(new HMACSHA256Algorithm())
               .WithSecret(ConfigurationManager.AppSettings["PolicyServiceSecret"])
               .AddClaim(ClaimName.ExpirationTime.ToString(), DateTime.UtcNow.AddMinutes(2))
               .AddClaim(ClaimName.NotBefore.ToString(), DateTime.UtcNow.AddSeconds(2))
               .Build();

            var context = new HttpActionContext();
            var request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue("jwt", token);
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;
            context.ControllerContext = controllerContext;

            //Act
            var isAuthenticated = base.IsAuthorized(context);

            //Assert
            Assert.IsFalse(isAuthenticated);
        }

        [TestMethod]
        public void AuthenticateAttribute_Expired_ExpirationTime_Claim_Returns_False()
        {
            //Arrange

            var token = new JwtBuilder()
               .WithAlgorithm(new HMACSHA256Algorithm())
               .WithSecret(ConfigurationManager.AppSettings["PolicyServiceSecret"])
               .AddClaim(ClaimName.ExpirationTime.ToString(), DateTime.UtcNow.AddMinutes(-2))
               .AddClaim(ClaimName.NotBefore.ToString(), DateTime.UtcNow.AddSeconds(-2))
               .Build();

            var context = new HttpActionContext();
            var request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue("jwt", token);
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;
            context.ControllerContext = controllerContext;

            //Act
            var isAuthenticated = base.IsAuthorized(context);

            //Assert
            Assert.IsFalse(isAuthenticated);
        }
    }
}