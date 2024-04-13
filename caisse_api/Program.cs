var builder = WebApplication.CreateBuilder(args);
var build = new ConfigurationBuilder();
build.AddJsonFile("appsettings.json");
var config = build.Build();

builder.Services.Configure<DbContextSettings>(config);
builder.Services.AddSingleton<IDbContextCaisse,DbContextCaisse>();
builder.Services.AddSingleton(typeof(IService<>),typeof(Service<>));
builder.Services.AddControllers().AddNewtonsoftJson(option=>
option.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.InjectionHelpers();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


