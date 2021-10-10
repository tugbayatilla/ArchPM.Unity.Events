using System;
using System.Linq;

namespace ArchPM.Unity.Events.Models
{
	internal class Consumer : IConsumer
	{
		private readonly EventBus _eventBus;

		public Consumer(EventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public int ActiveCount => _eventBus.ActiveConsumerCount;

		public IConsumerResult Consume<T>(Action<EventMessage<T>> action) where T : IEvent
		{
			EventHandler<EventMessage> internalAction = null;

			internalAction = (_, eventMessage) =>
			{
				if (eventMessage.EventType == typeof(T))
				{
					//convertion to generic class
					var genericEvent = EventMessage<T>.Convert(eventMessage);
					try
					{
						action(genericEvent);
					}
					catch (Exception ex)
					{
						_eventBus.LogError(ex);
					}
				}
			};

			_eventBus.OnEventPublished += internalAction;

			return ConsumerResult.NewResult(_eventBus, internalAction);
		}

		public IConsumerResult Consume(Action<EventMessage> action, params Type[] expectedTypes)
		{
			EventHandler<EventMessage> internalMethod = null;

			internalMethod = (_, eventMessage) =>
			{
				if (expectedTypes.Contains(eventMessage.EventType))
				{
					try
					{
						action(eventMessage);
					}
					catch (Exception ex)
					{
						_eventBus.LogError(ex);
					}
				}
			};

			_eventBus.OnEventPublished += internalMethod;

			return ConsumerResult.NewResult(_eventBus, internalMethod);
		}

		public void UnsubscribeAll()
		{
			_eventBus.UnsubscribeAll();
		}
	}
}
