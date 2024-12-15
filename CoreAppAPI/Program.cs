using Shared.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Module.User;
using Shared.Core.Extensions;
//using Module.Flow;
//using Module.Ticket;
//using Module.Notification;
//using Module.Notification.Core.Hubs;
//using Module.Notification.Infrastructure.Middlewares;
//using Module.Job;
//using Module.Package;
//using Module.Payment;
//using ManagexAPI.Configurations;
//using Module.Call;
//using Module.Email;
//using Module.Messaging;
//using Serilog;

using Microsoft.AspNetCore.WebSockets;

var builder = WebApplication.CreateBuilder(args);


#region SeriLog Code

builder.Services.AddLogging(builder =>
{
	builder.AddConsole();
});
//builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
//{
//	loggerConfiguration
//		.ReadFrom.Configuration(builder.Configuration);

//});

#endregion

builder.Services.AddUserModule(builder.Configuration);
builder.Services.AddSharedCore();
builder.Services.AddSharedInfrastructure(builder.Configuration);

//builder.Services.AddFlowModule(builder.Configuration);
//builder.Services.AddTicketModule(builder.Configuration);
//builder.Services.AddNotificationModule(builder.Configuration);
//builder.Services.AddJobModule(builder.Configuration);
//builder.Services.AddMessageConfiguration();
//builder.Services.AddPackageModule(builder.Configuration);
//builder.Services.AddPaymentModule(builder.Configuration);
//builder.Services.AddCallModule(builder.Configuration);
//builder.Services.AddEmailModule(builder.Configuration);
//builder.Services.AddMessagingModule(builder.Configuration);

// Messaging 
//builder.Services.AddMassTransit(config =>
//{
//    config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
//    {
//        cfg.Host("localhost", "/", h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });
//    }));
//});

//builder.Services.AddMassTransit(x =>
//{
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
//    {
//        cfg.Host(new Uri("rabbitmq://localhost"), h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });
//    }));
//});




builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

	// TO RUN APIS ON SWAGGER WITH AUTHORIZATION HEADER

	//c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	//{
	//    In = ParameterLocation.Header,
	//    Description = "Please enter a valid token",
	//    Name = "Authorization",
	//    Type = SecuritySchemeType.Http,
	//    BearerFormat = "JWT",
	//    Scheme = "Bearer"
	//});
	//c.AddSecurityRequirement(new OpenApiSecurityRequirement
	//{
	//    {
	//        new OpenApiSecurityScheme
	//        {
	//            Reference = new OpenApiReference
	//            {
	//                Type=ReferenceType.SecurityScheme,
	//                Id="Bearer"
	//            }
	//        },
	//        new string[]{}
	//    }
	//});

});


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	builder.WebHost.UseUrls("http://0.0.0.0:7285");
}
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

//app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(x => x
				.AllowAnyMethod()
				.AllowAnyHeader()
				.SetIsOriginAllowed(origin => true) // allow any origin
				);

app.UseMiddleware<WebSocketMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

//app.MapHub<StreamHub>("/session-hub");
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});


app.Run();
