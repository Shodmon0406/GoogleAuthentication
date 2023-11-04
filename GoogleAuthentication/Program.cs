using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;

builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie")
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "525656628108-600vlq66hvukemhp5o1l1ret5e0olpkq.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
    });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/login", () =>
{
    return Results.Challenge(
        new AuthenticationProperties()
        {
            RedirectUri = "http://localhost:5201/"
        },
        authenticationSchemes: new List<string>() { "google" });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();