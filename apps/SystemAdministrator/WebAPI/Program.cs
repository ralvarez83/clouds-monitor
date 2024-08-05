using Azure.Core.Diagnostics;
using Azure.Identity;
using WebAPI.Extensions.DependencyInjection;
// Setup a listener to monitor logged events.
using AzureEventSourceListener listener = AzureEventSourceListener.CreateConsoleLogger();

var builder = WebApplication.CreateBuilder(args);

var value = System.Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");

DefaultAzureCredentialOptions options = new DefaultAzureCredentialOptions()
{
    Diagnostics =
    {
        LoggedHeaderNames = { "x-ms-request-id" },
        LoggedQueryParameters = { "api-version" },
        IsLoggingContentEnabled = true
    }
};

// Add services to the container.
// builder.Services.AddAzureClients(static x =>
// {
//     TokenCredential cred = new DefaultAzureCredential();
//     ArmClient client = new ArmClient(cred);
//     x.AddArmClient(client.GetDefaultSubscription().ToString());
//     x.UseCredential(new DefaultAzureCredential());
// });

builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfraestructure();

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
