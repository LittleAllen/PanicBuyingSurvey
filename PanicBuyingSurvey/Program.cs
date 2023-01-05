using PanicBuyingSurvey.DataLayer;
using PanicBuyingSurvey.Filters;
using PanicBuyingSurvey.Middlewares;
using PanicBuyingSurvey.Services;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("config/secret.json");
        builder.Configuration.AddJsonFile("config/serilog.json");
        builder.Host.UseSerilog((context, services, config) =>
            config.ReadFrom.Configuration(context.Configuration));
        // Add services to the container.
        builder.Services.AddSingleton<DapperContext>();
        builder.Services.AddScoped<IProductDataLayer, ProductDataLayer>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddControllers(config =>{
            config.Filters.Add(new FormatJsonResultFilter());
        });
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
        
        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.MapControllers();

        app.Run();
    }
}