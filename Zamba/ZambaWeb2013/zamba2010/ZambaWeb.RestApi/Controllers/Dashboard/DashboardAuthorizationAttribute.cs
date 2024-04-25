using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.UI;

public class DashboardAuthorizationAttribute : AuthorizeAttribute
{

    public override void OnAuthorization(HttpActionContext actionContext)
    {
        var authHeader = actionContext.Request.Headers.Authorization;
        if (authHeader != null && authHeader.Scheme == "Bearer" && !string.IsNullOrEmpty(authHeader.Parameter))
        {
            var token = authHeader.Parameter;
            // Aquí puedes usar el token para la autenticación/autorización
            if(new JWTManager().ValidateJwtToken(token))
                return;
        }
        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "ZambaRRHH Unathorized");
    }
}