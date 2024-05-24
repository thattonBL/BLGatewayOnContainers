using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Common.Events
{
    public abstract record BaseRsiMessageSubmittedIntegrationEvent : IntegrationEvent
    {

        public static string EVENT_NAME = "NewRsiMessageSubmitted.IntegrationEvent";
        public BaseRsiMessageSubmittedIntegrationEvent(RsiPostItem rsiMessage, string eventName)
        {
            RsiMessage = rsiMessage;
            EventName = eventName;
        }

        public RsiPostItem RsiMessage { get; init; }

        public string EventName { get; init; }
    }
}
