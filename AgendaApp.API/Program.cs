using AgendaApp.API.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurações do AutoMapper
builder.Services.AddAutoMapper(typeof(ProfileMap));

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefultPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("DefaultPolicy");

app.MapControllers();

app.Run();
