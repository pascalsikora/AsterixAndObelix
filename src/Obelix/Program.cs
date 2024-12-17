using AsterixAndObelix.Obelix.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(configure =>
    {
        configure.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("localhost", "/", host =>
            {
                host.Username("guest");
                host.Password("guest");
            });

            cfg.ConfigureEndpoints(context);
        });
        configure.AddConsumer<MessageRequestConsumer>();
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
