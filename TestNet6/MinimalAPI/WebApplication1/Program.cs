using Extensions;
using Controllers;
using BE;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("App"));
builder.Services.AddDepndencys();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseUserMiddlewares();
app.UtilRoutes();

app.Run();
