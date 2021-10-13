using System;

namespace ArchPM.Unity.Events.Models
{
	internal class ConsumeResult : IConsumeResult
	{
		private readonly EventBus _eventBus;
		private readonly EventHandler<EventMessage> _eventHandler;

		public ConsumeResult(EventBus eventBus, EventHandler<EventMessage> eventHandler)
		{
			_eventBus = eventBus;
			_eventHandler = eventHandler;
		}

		public void Unsubscribe()
		{
			_eventBus.Unsubscribe(_eventHandler);
		}

		public static IConsumeResult NewResult(EventBus eventBus, EventHandler<EventMessage> eventHandler)
			=> new ConsumeResult(eventBus, eventHandler);
	}
}
