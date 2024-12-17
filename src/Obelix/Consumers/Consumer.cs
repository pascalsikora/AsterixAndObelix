namespace AsterixAndObelix.Obelix.Consumers;

using System.Threading.Tasks;
using AsterixAndObelix.Shared;
using MassTransit;
using Microsoft.Extensions.Logging;

sealed class MessageRequestConsumer : IConsumer<MessageRequest>
{
    private readonly ILogger<MessageRequestConsumer> logger;

    public MessageRequestConsumer(ILogger<MessageRequestConsumer> logger)
    {
        this.logger = logger;
    }

    public async Task Consume(ConsumeContext<MessageRequest> context)
    {
        this.logger.LogInformation("Received Text: {Text}", context.Message.Payload);
        MessageResponse messageResponse = new()
        {
            Payload = context.Message.Payload,
        };
        await context.RespondAsync<MessageResponse>(messageResponse);
    }
}
