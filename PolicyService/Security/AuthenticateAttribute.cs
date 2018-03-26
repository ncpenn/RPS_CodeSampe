using JWT;
using JWT.Builder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Service.PolicyService.Security
{
    public class AuthenticateAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            string token = actionContext.Request?.Headers?.Authorization?.Parameter;

            try
            {
                var payload = new JwtBuilder()
                    .WithSecret(Config.PolicyServiceSecret)
                    .MustVerifySignature()
                    .Decode(token);

                var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
                var notBefore = DateTime.Parse(claims[ClaimName.NotBefore.ToString()], CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                var expiry = DateTime.Parse(claims[ClaimName.ExpirationTime.ToString()], CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

                if (DateTime.UtcNow < notBefore || DateTime.UtcNow > expiry)
                {
                    return false;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (SignatureVerificationException)
            {
                return false;
            }

            return true;
        }
    }
}