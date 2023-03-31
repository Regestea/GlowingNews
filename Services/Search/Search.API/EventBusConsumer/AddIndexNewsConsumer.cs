using EventBus.Messages.Events;
using MassTransit;
using Nest;
using Search.API.Models;

namespace Search.API.EventBusConsumer
{
    public class AddIndexNewsConsumer: IConsumer<AddNewsToSearchIndexEvent>
    {
        private readonly IElasticClient _elasticClient;

        public AddIndexNewsConsumer(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<AddNewsToSearchIndexEvent> context)
        {
            var news = new News()
            {
                Id = context.Message.NewsId,
                Text = context.Message.Text
            };
            await _elasticClient.IndexAsync<News>(news, x => x);

        }
    }
}
