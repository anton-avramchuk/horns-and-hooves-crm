using HornsAndHoovesCrm.AspNetCore.Extensions;
using HornsAndHoovesCrm.Core.Extensions.DependencyInjection;
using HornsAndHoovesCrm.Host;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication<BootstrapModule>();

var app = builder.Build();
app.InitializeApplication();



app.UseHttpsRedirection();



app.Run();

