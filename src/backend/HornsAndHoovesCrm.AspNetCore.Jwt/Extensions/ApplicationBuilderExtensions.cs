using HornsAndHoovesCrm.AspNetCore.Extensions;
using HornsAndHoovesCrm.AspNetCore.Jwt.Models;
using HornsAndHoovesCrm.AspNetCore.Jwt.Options;
using HornsAndHoovesCrm.AspNetCore.Jwt.Services;
using HornsAndHoovesCrm.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HornsAndHoovesCrm.AspNetCore.Jwt.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app,
        string schema = JwtBearerDefaults.AuthenticationScheme)
    {
        return app.Use(async (ctx, next) =>
        {
            if (ctx.User.Identity?.IsAuthenticated != true)
            {
                var result = await ctx.AuthenticateAsync(schema);
                if (result.Succeeded && result.Principal != null)
                {
                    ctx.User = result.Principal;
                }
            }

            await next();
        });
    }


    public static void UseJwtAuthModel<TModel>(this ApplicationInitializationContext context) where TModel : ILoginModel
    {
        var options = context.GetOptions<JwtAuthOptions>();

        var routeBuilder = context.GetRouteBuilder();
        routeBuilder.MapPost(options.AuthPath, async ([FromBody] TModel model, ILoginService<TModel> service) =>
        {
            var result = await service.LoginAsync(model);

            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.BadRequest();
        });
    }
}