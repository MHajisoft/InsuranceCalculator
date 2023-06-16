using System.Text;
using Insurance.Common.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Insurance.Api.Configuration;

public static class AuthenticationConfiguration
{
    public static Action<AuthenticationOptions> GetJwtAuthenticationOption()
    {
        return config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        };
    }

    public static Action<JwtBearerOptions> GetJwtBearerOptions()
    {
        var key = Encoding.ASCII.GetBytes(InsuranceConstant.SecurityKey);

        return config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                //ToDo بهتر است دو مورد زیر هم فعال شود
                ValidateIssuer = false,
                ValidateAudience = false,
                // ValidIssuer = null,
                // ValidAudience = null
            };
            config.Events = new JwtBearerEvents
            {
                //ToDo بازنگری عملکرد در این قسمت
                OnAuthenticationFailed = ExceptionHandlingConfiguration.OnJwtAuthenticationFailed,
                OnForbidden = ExceptionHandlingConfiguration.OnJwtForbidden,
                OnChallenge = ExceptionHandlingConfiguration.OnJwtChallenge
            };
        };
    }
}