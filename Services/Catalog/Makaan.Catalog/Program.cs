using Makaan.Catalog.Services.ContactIntroPosterServices;
using Makaan.Catalog.Services.ContactServices;
using Makaan.Catalog.Services.EstateAgentApplicationServices;
using Makaan.Catalog.Services.FeaturedAboutServices;
using Makaan.Catalog.Services.IntroSliderAreaServices;
using Makaan.Catalog.Services.IntroTextAreaServices;
using Makaan.Catalog.Services.PropertyAgentServices;
using Makaan.Catalog.Services.PropertyDetailServices;
using Makaan.Catalog.Services.PropertyImageServices;
using Makaan.Catalog.Services.PropertyIntroPosterServices;
using Makaan.Catalog.Services.PropertyServices;
using Makaan.Catalog.Services.PropertyTypeServices;
using Makaan.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "resource_catalog";
});

builder.Services.AddScoped<IIntroTextAreaService, IntroTextAreaService>();
builder.Services.AddScoped<IIntroSliderAreaService, IntroSliderAreaService>();
builder.Services.AddScoped<IPropertyTypeService, PropertyTypeService>();
builder.Services.AddScoped<IFeaturedAboutService, FeaturedAboutService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyAgentService, PropertyAgentService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IPropertyIntroPosterService, PropertyIntroPosterService>();
builder.Services.AddScoped<IContactIntroPosterService, ContactIntroPosterService>();
builder.Services.AddScoped<IPropertyDetailService, PropertyDetailService>();
builder.Services.AddScoped<IPropertyImageService, PropertyImageService>();
builder.Services.AddScoped<IEstateAgentApplicationService, EstateAgentApplicationService>();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
