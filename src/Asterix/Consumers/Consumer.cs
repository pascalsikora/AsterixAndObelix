namespace AsterixAndObelix.Asterix.Consumers;

using System.Threading.Tasks;
using AsterixAndObelix.Shared;
using MassTransit;
using Microsoft.Extensions.Logging;

sealed class MessageResponseConsumer : IConsumer<MessageResponse>
{
    private readonly ILogger<MessageResponseConsumer> logger;

    public MessageResponseConsumer(ILogger<MessageResponseConsumer> logger)
    {
        this.logger = logger;
    }

    public Task Consume(ConsumeContext<MessageResponse> context)
    {
        this.logger.LogInformation("Received response Text: {Text}", context.Message.Payload);

        return Task.CompletedTask;
    }
}
