using Serilog;
using WebAPI;



var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

// Add services to the container.
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

startup.ConfigureServices(builder.Services);
builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.RollingFile(".\\logs\\log-{Date}.txt", shared: true
               // ,outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {EscapedMessage}{NewLine}{EscapedException}"
            ));
var app = builder.Build();

startup.Configure(app, builder.Environment);