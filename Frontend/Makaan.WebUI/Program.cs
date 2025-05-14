using Makaan.WebUI.Areas.Admin.Hubs;
using Makaan.WebUI.Handlers;
using Makaan.WebUI.Services.AccountNotificationServices;
using Makaan.WebUI.Services.ApplicationNotificationServices;
using Makaan.WebUI.Services.ClientCredentialsTokenServices;
using Makaan.WebUI.Services.CommentNotificationServices;
using Makaan.WebUI.Services.ContactIntroPosterServices;
using Makaan.WebUI.Services.ContactServices;
using Makaan.WebUI.Services.EstateAgentApplicationServices;
using Makaan.WebUI.Services.FeaturedAboutServices;
using Makaan.WebUI.Services.IntroSliderAreaServices;
using Makaan.WebUI.Services.IntroTextAreaServices;
using Makaan.WebUI.Services.MemberCommentServices;
using Makaan.WebUI.Services.MessageServices.ReceivedMessageServices;
using Makaan.WebUI.Services.MessageServices.SenderedMessageServices;
using Makaan.WebUI.Services.NotificationServices.AccountNotificationServices;
using Makaan.WebUI.Services.NotificationServices.ApplicationNotificationServices;
using Makaan.WebUI.Services.NotificationServices.CommentNotificationServices;
using Makaan.WebUI.Services.PasswordTokenServices;
using Makaan.WebUI.Services.PropertyAgentServices;
using Makaan.WebUI.Services.PropertyDetailServices;
using Makaan.WebUI.Services.PropertyImageServices;
using Makaan.WebUI.Services.PropertyIntroPosterServices;
using Makaan.WebUI.Services.PropertyServices;
using Makaan.WebUI.Services.PropertyTypeServices;
using Makaan.WebUI.Services.SignInServices;
using Makaan.WebUI.Services.SignUpServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Makaan.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpClient();
builder.Services.AddAccessTokenManagement();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

builder.Services.AddAuthentication(opt=>
{
    opt.DefaultScheme = "Cookies";
}).AddCookie("Cookies",opt=>
{
    opt.LoginPath = "/Account/Login";
    opt.AccessDeniedPath = "/AccesDenied/Index";
});

//ClientCredentials
builder.Services.AddHttpClient<IClientCredentialsTokenService,ClientCredentialsTokenService>();
builder.Services.AddTransient<ClientCredentialTokenHandler>();

//Password
builder.Services.AddScoped<IPasswordTokenService,PasswordTokenService>();
builder.Services.AddTransient<PasswordTokenHandler>();

//Settings
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//SignIn
builder.Services.AddScoped<ISignInService, SignInService>();

var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

//SingUp
builder.Services.AddHttpClient<ISignUpService, SignUpService>(opt=>
{
    opt.BaseAddress = new Uri($"{values.IdentityServerUrl}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();


//IntroTextArea
builder.Services.AddHttpClient<IIntroTextAreaService, IntroTextAreaService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//IntroTextArea
builder.Services.AddHttpClient<IIntroSliderAreaService, IntroSliderAreaService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//PropertyType
builder.Services.AddHttpClient<IPropertyTypeService, PropertyTypeService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//FeaturedAbout
builder.Services.AddHttpClient<IFeaturedAboutService, FeaturedAboutService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//Property
builder.Services.AddHttpClient<IPropertyService, PropertyService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//PropertyAgent
builder.Services.AddHttpClient<IPropertyAgentService, PropertyAgentService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//MemberComment
builder.Services.AddHttpClient<IMemberCommentService, MemberCommentService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//MemberComment
builder.Services.AddHttpClient<IContactService, ContactService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//PropertyIntroPoster
builder.Services.AddHttpClient<IPropertyIntroPosterService, PropertyIntroPosterService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//ContactIntroPoster
builder.Services.AddHttpClient<IContactIntroPosterService, ContactIntroPosterService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//User
builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.IdentityServerUrl}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//UserIdentity
builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.IdentityServerUrl}");
}).AddHttpMessageHandler<PasswordTokenHandler>();

//PropertyDetail
builder.Services.AddHttpClient<IPropertyDetailService, PropertyDetailService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//PropertyImage
builder.Services.AddHttpClient<IPropertyImageService, PropertyImageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//PropertyImage
builder.Services.AddHttpClient<IPropertyImageService, PropertyImageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//EstateAgentApplication
builder.Services.AddHttpClient<IEstateAgentApplicationService, EstateAgentApplicationService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//CommentNotification
builder.Services.AddHttpClient<ICommentNotificationService, CommentNotificationService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Notification.Path}");
}).AddHttpMessageHandler<PasswordTokenHandler>();

//ApplicationNotification
builder.Services.AddHttpClient<IApplicationNotificationService, ApplicationNotificationService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Notification.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//AccountNotification
builder.Services.AddHttpClient<IAccountNotificationService, AccountNotificationService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Notification.Path}");
}).AddHttpMessageHandler<PasswordTokenHandler>();

//ReceivedMessage
builder.Services.AddHttpClient<IReceivedMessageService, ReceivedMessageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
}).AddHttpMessageHandler<PasswordTokenHandler>();

//SenderedMessage
builder.Services.AddHttpClient<ISenderedMessageService, SenderedMessageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
}).AddHttpMessageHandler<PasswordTokenHandler>();

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

app.MapHub<StatisticHub>("/statistichub");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
