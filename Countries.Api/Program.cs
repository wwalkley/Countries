var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services
    .AddControllers( )
    .AddNewtonsoftJson( )
    .AddXmlDataContractSerializerFormatters( );

builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );

var app = builder.Build( );

// Configure the HTTP request pipeline.

app.UseSwagger( );
app.UseSwaggerUI( );

app.UseHttpsRedirection( );

app.UseAuthorization( );

app.MapControllers( );

app.Run( );