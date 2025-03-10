using Microsoft.AspNetCore.Builder;

namespace HornsAndHoovesCrm.OpenApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseOpenApiClient(this WebApplication app)
    {
        app.MapOpenApi();
        return app;
    }
}