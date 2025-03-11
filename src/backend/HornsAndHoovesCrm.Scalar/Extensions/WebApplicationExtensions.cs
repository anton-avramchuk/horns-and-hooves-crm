using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace HornsAndHoovesCrm.Scalar.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseScalarOpenApiClient(this WebApplication app)
    {
        app.MapGet("/", context =>
        {
            context.Response.Redirect("/scalar", permanent: false);
            return Task.CompletedTask;
        });
        
        
        app.MapScalarApiReference();
        return app;
    }
}