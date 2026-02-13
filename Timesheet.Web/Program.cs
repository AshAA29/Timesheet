using Timesheet.Infrastructure;
using Timesheet.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTimesheet();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();        

app.UseAuthentication();     
app.UseAuthorization();

app.UseAntiforgery();        

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();