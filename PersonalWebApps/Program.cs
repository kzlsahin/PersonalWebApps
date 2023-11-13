using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tracker_webapp.Data;
using tracker_webapp.Areas.Identity.Data;
using System.Net.Mail;
using System.Net;
using tracker_webapp.Communication;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("tracker_webappContextConnection") ?? throw new InvalidOperationException("Connection string 'tracker_webappContextConnection' not found.");
var itemStoreConncetionString = builder.Configuration.GetConnectionString("tracker_itemStoreContextConnection") ?? throw new InvalidOperationException("Connection string 'tracker_webappContextConnection' not found.");

builder.Services.AddDbContext<tracker_webappContext>(
    options => options.UseSqlServer(connectionString)
    );
builder.Services.AddDbContext<ItemsDBContext>(
    options => options.UseSqlServer(itemStoreConncetionString)
    );

builder.Services
    .AddDefaultIdentity<tracker_webappUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<tracker_webappContext>()
.AddDefaultUI();

builder.Services.AddTransient<IEmailSender, EmailSender>(
    (smtpClient) =>
    new EmailSender("smtp.gmail.com", 587, new MailAddress("senturkmus@gmail.com"), new NetworkCredential("senturkmus@gmail.com", "pjupnwbonpgugsjv"), true)
    );

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
app.UseDefaultFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Identity",
    areaName: "Identity",
    pattern: "Identity/{controller}/{action}/{id?}");
app.MapAreaControllerRoute(
    name: "Tracker",
    areaName: "Tracker",
    pattern: "Tracker/{controller}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    name: "ReportEditor",
    areaName: "ReportEditor",
    pattern: "ReportEditor/{controller=Editor}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
