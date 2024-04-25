using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Spire.License;
using static ZambaWeb.RestApi.Controllers.Dashboard.DB.DashboardDatabase;

public class JWTManager
{
    private const string ISSUER = "StardocRRHH";
    private const string AUDIENCE= "ZambaDashboardRRHH";
    private static string SECRET_KEY { get; set; } = System.Configuration.ConfigurationManager.AppSettings["JWT-Key"];
    private static string VALID_DURATION_DAYS { get; set; } = System.Configuration.ConfigurationManager.AppSettings["JWTValidityInDays"];
    public string UserId { get; private set; }
    public string ZambaUserId { get; private set; }
    public string ZambaToken { get; private set; }

    public JWTManager()
    {
        
    }

    public JWTManager(HttpRequestMessage request)
    {
        var authorizationHeader = request.Headers.Authorization;
        if (authorizationHeader != null && authorizationHeader.Scheme == "Bearer")
        {
            var token = authorizationHeader.Parameter;
            var principal = GetPrincipalFromToken(token);

            UserId = principal?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            ZambaUserId = principal?.Claims.FirstOrDefault(c => c.Type == "ZambaUserId")?.Value;
            ZambaToken = principal?.Claims.FirstOrDefault(c => c.Type == "zambaToken")?.Value;
        }
    }
    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        if (jwtToken == null)
        {
            return null;
        }

        var identity = new ClaimsIdentity();
        foreach (var claim in jwtToken.Claims)
        {
            identity.AddClaim(claim);
        }

        var principal = new ClaimsPrincipal(identity);
        return principal;
    }
    public void CreateJwtToken(UserDTOLogin user)
    {
        int days = 0;
        int.TryParse(VALID_DURATION_DAYS, out days);

        var claims = new[]
        {
            new Claim("UserId",user.id.ToString()),
            new Claim("ZambaUserId",user.userid.ToString()),
            new Claim("zambaToken",user.token),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SECRET_KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: ISSUER,
            audience: AUDIENCE,
            claims: claims,
            expires: DateTime.Now.AddDays(days),
            signingCredentials: creds);

        user.durationDays = days;
        user.jwt = new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = ISSUER,
            ValidateAudience = true,
            ValidAudience = AUDIENCE,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SECRET_KEY)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
