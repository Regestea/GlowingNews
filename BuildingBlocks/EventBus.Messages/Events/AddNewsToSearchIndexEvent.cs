using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class AddNewsToSearchIndexEvent:IntegrationBaseEvent
    {
        public Guid NewsId { get; set; }
        public string Text { get; set; } = null!;
    }
}
