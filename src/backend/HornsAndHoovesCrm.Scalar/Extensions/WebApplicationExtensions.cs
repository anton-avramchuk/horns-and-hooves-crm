using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace HornsAndHoovesCrm.Scalar.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseScalarOpenApiClient(this WebApplication app)
    {
        app.MapGet("/", async context =>
        {
            // Здесь вы можете перенаправить на нужную страницу Scalar API
            // Например, если это страница swagger:
            context.Response.Redirect("/scalar", permanent: false);
        });
        
        
        app.MapScalarApiReference();
        return app;
    }
}