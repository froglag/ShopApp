using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Shop.DataBase;
using Stripe;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ShopApp"));
});

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "AntiforgeryCookieName";
    options.FormFieldName = "AntiforgeryFieldName";
    options.HeaderName = "AntiforgeryHeaderName";
    options.SuppressXFrameOptionsHeader = false;

    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


builder.Services.AddDistributedMemoryCache();

builder.Services.AddAuthentication();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddAuthorization();



builder.Services.AddSession(options =>
{
    options.Cookie.Name = "cart";
    options.Cookie.Name = "customer-info";
    options.Cookie.MaxAge = TimeSpan.FromMinutes(120);

    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();
app.UseSession();

app.Run();
