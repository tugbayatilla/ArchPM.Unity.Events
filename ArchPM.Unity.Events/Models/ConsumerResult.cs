using System;

namespace ArchPM.Unity.Events.Models
{
	internal class ConsumerResult : IConsumerResult
	{
		private readonly EventBus _eventBus;
		private readonly EventHandler<EventMessage> _eventHandler;

		public ConsumerResult(EventBus eventBus, EventHandler<EventMessage> eventHandler)
		{
			_eventBus = eventBus;
			_eventHandler = eventHandler;
		}

		public void Unsubscribe()
		{
			_eventBus.Unsubscribe(_eventHandler);
		}

		public static IConsumerResult NewResult(EventBus eventBus, EventHandler<EventMessage> eventHandler)
			=> new ConsumerResult(eventBus, eventHandler);
	}
}
