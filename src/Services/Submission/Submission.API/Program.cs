using Submission.API;
using Submission.API.Endpoints;
using Submission.Application;
using Submission.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Add Services
builder.Services
    .AddApiServices(builder.Configuration)
    .AddAplicationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);
#endregion

var app = builder.Build();

#region Use Services
app
    .UseSwagger()
    .UseSwaggerUI()
    .UseRouting(); // match the HTTP request to an endpoint (route) based on the URL

app.MapAllEndpoints();
// TO-DO migrate - create first migration

if (app.Environment.IsDevelopment())
{
    // TO-DO - seed test data
}

#endregion

app.Run();


