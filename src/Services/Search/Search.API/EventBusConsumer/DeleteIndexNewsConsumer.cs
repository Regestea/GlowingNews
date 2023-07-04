using EventBus.Messages.Events;
using MassTransit;
using Nest;
using Search.API.Models;

namespace Search.API.EventBusConsumer
{
    public class DeleteIndexNewsConsumer: IConsumer<DeleteNewsFromSearchIndexEvent>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteIndexNewsConsumer(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<DeleteNewsFromSearchIndexEvent> context)
        {
            Console.WriteLine("Delete Consume");
            await _elasticClient.DeleteAsync<News>(context.Message.NewsId);
        }
    }
}
