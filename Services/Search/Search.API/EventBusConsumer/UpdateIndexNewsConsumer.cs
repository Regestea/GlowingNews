using EventBus.Messages.Events;
using MassTransit;
using Nest;
using Search.API.Models;

namespace Search.API.EventBusConsumer
{
    public class UpdateIndexNewsConsumer:IConsumer<UpdateNewsFromSearchIndexEvent>
    {
        private readonly IElasticClient _elasticClient;

        public UpdateIndexNewsConsumer(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<UpdateNewsFromSearchIndexEvent> context)
        {

            Console.WriteLine("Update Consume");
            var response = await _elasticClient.UpdateAsync<News>(context.Message.NewsId, u => u
                .Doc(new News() { Id = context.Message.NewsId, Text = context.Message.Text })
            );

        }
    }
}
