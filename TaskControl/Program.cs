using Microsoft.EntityFrameworkCore;
using TaskControl.Entities;
using Microsoft.Data.SqlClient;
using AutoMapper;
using TaskControl.Helpers;

var builder = WebApplication.CreateBuilder(args);


var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));

var connectionString = conStrBuilder.ConnectionString;


// TODO Check mapper config.
var mapperConfig = new MapperConfiguration(cfg => 
{
    cfg.AddProfile(new MappingProfiles());

});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddDbContext<TaskControlContext>(c => c.UseSqlServer(connectionString));

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
