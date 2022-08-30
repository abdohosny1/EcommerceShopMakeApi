


using EcommerceShop.Api.Errors;
using EcommerceShop.Api.Extensision;
using EcommerceShop.Api.MiddleWare;
using StackExchange.Redis;

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


//add extesnsison service
builder.Services.AddApplicationService();
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

app.UseMiddleware<ExpectionMiddelWare>();
// Configure the HTTP request pipeline.

// app.UseDeveloperExceptionPage();
app.UseSweggerDoc();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors(text);


app.MapControllers();
ApplicatioContextSeeding.Seed(app);

app.Run();
