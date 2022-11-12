using Countries.Api.Stores;
using Serilog;

Log.Logger = new LoggerConfiguration( )
    .MinimumLevel.Debug( )
    .WriteTo.Console( )
    .WriteTo.File( "logs/log.txt", rollingInterval: RollingInterval.Day )
    .CreateLogger( );

var builder = WebApplication.CreateBuilder( args );

builder.Host.UseSerilog( );

// Add services to the container.
builder.Services
    .AddControllers( )
    .AddNewtonsoftJson( )
    .AddXmlDataContractSerializerFormatters( );


builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );

builder.Services.AddSingleton<DataStore>( );

var app = builder.Build( );

// Configure the HTTP request pipeline.

app.UseSwagger( );
app.UseSwaggerUI( );

app.UseHttpsRedirection( );

app.UseAuthorization( );

app.MapControllers( );

app.Run( );