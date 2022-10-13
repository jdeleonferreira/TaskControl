using Microsoft.EntityFrameworkCore;
using TaskControl.Entities;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);


var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"))
                    {
                        Password = builder.Configuration["DbPassword"]
                    };


// TODO Check mapper config.
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddDbContext<TaskControlContext>(c => c.UseSqlServer(conStrBuilder.ConnectionString));

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
