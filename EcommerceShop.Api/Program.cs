


using EcommerceShop.Api.Errors;
using EcommerceShop.Api.Extensision;
using EcommerceShop.Api.MiddleWare;
using EcommerceShop.Core.Model.sendingEmail;
using EcommerceShop.Core.Model.sendSMS;
using EcommerceShop.EF.Identity;
using StackExchange.Redis;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

//add cors 
string text = "";


// Add services to the container.
builder.Services.AddControllers();

//add dbcontext
var connection = builder.Configuration.GetConnectionString("DefultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(
    op => op.UseSqlServer(connection,
           b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));



/// add dbcontext for identity
builder.Services.AddDbContext<AppIdentityDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("IdenetityConnection"));
}
    );

//add readis
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var configure = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
    return ConnectionMultiplexer.Connect(configure);
});

//add auto maper service
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// add token
builder.Services.Configure<Tokenn>(builder.Configuration.GetSection("Token"));

// sending emaill
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));

//add Twillio
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));


//add extesnsison service
builder.Services.AddApplicationService();
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddSwaggerDocumantion();


//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(text,
    builder =>
    {
        builder.AllowAnyOrigin();
        //builder.WithOrigins("url");
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

//app.UseMiddleware<ExpectionMiddelWare>();
// Configure the HTTP request pipeline.

// app.UseDeveloperExceptionPage();
app.UseSweggerDoc();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(text);

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
ApplicatioContextSeeding.Seed(app);
//AppIdentityContextSeeding.SeedUsersAndRolesAsync(app).Wait();

app.Run();
