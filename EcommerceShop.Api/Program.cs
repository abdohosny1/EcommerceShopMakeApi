


using EcommerceShop.Api.Errors;
using EcommerceShop.Api.Extensision;
using EcommerceShop.Api.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//add dbcontext
var connection = builder.Configuration.GetConnectionString("DefultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(
    op => op.UseSqlServer(connection,
           b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));


//add auto maper service
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//add extesnsison service
builder.Services.AddApplicationService();
builder.Services.AddSwaggerDocumantion();

var app = builder.Build();

app.UseMiddleware<ExpectionMiddelWare>();
// Configure the HTTP request pipeline.

// app.UseDeveloperExceptionPage();
app.UseSweggerDoc();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
ApplicatioContextSeeding.Seed(app);

app.Run();
