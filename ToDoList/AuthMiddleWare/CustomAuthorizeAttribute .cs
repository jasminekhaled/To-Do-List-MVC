using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ToDoList.AuthMiddleWare
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Get token from cookie
            var token = context.HttpContext.Request.Cookies["token"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectResult("/Home/Index");
                return;
            }

            try
            {
                // Decode the token
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken == null)
                {
                    // Token decoding failed, redirect to login page
                    context.Result = new RedirectResult("/Home/Index");
                    return;
                }
                // Extract user ID from claims
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId");

                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    // User ID not found in token, redirect to sign up page or handle as needed
                    context.Result = new RedirectResult("/Auth/SignUp");
                    return;
                }
                // Set the user ID in HttpContext.Items
                context.HttpContext.Items["userId"] = userIdClaim.Value;
                return;
            }
            catch (Exception)
            {
                // Token decoding or validation failed, redirect to login page
                context.Result = new RedirectResult("/Home/Index");
                return;
            }
        }









        /* public void OnAuthorization(AuthorizationFilterContext context)
         {
             // get token from cookie

             var token = context.HttpContext.Request.Cookies["token"];
             if (string.IsNullOrEmpty(token))
             {
                 context.Result = new RedirectResult("/Home/Index");
             }

             try
             {
                 // Decode the token
                 var tokenHandler = new JwtSecurityTokenHandler();
                 var jwtToken = tokenHandler.ReadJwtToken(token);
                 if (jwtToken == null)
                 {
                     // Token decoding failed, redirect to login page
                     context.Result = new RedirectResult("/Home/Index");
                     return;
                 }

                 // Extract user ID from claims
                 var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

                 if (string.IsNullOrEmpty(userId))
                 {
                     // User ID not found in token, redirect to login page
                     context.Result = new RedirectResult("/Auth/SignUp");
                     return;
                 }

                 // User ID found, continue with authorization logic
                 // You can perform additional authorization checks here if needed

                 return;
             }
             catch (Exception)
             {
                 // Token decoding or validation failed, redirect to login page
                 context.Result = new RedirectResult("/Home/Index");
                 return;
             }
         }*/
    }
}
