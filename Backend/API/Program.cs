global using DL;
global using Models;
using Backend.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
builder.Services.AddSignalR(); //Added for SignalR (Order matters make sure it is before .AddController())
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<Population>>(repo => new ExcelRepository("./DL/WPP2019_TotalPopulationBySex.xlsx"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();


app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.UseAuthorization();

//Added to configure that we will have more endpoints (Essentially we added a SignalR endpoint)
app.UseEndpoints(
    endpoints => {
        endpoints.MapControllers();
        endpoints.MapHub<ChartHub>("/chart"); //This is the SignalR endpoint
    }
);

app.Run();
