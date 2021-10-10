using ArchPM.Unity.Events.Models;
using System;

namespace ArchPM.Unity.Events
{
	public sealed class EventBus : IEventBus
	{
		// Members
		public static readonly IEventBus Instance = new EventBus();
		private readonly IPublisher _publisher;
		private readonly IConsumer _consumer;
		private readonly IInitialization _registerer;
		internal Action<string> _registeredLogInfoAction;
		internal Action<Exception> _registeredLogErrorAction;
		internal int ActiveConsumerCount => OnEventPublished?.GetInvocationList().Length ?? 0;
		internal event EventHandler<EventMessage> OnEventPublished;

		// Properties
		public IPublisher Publisher => _publisher;
		public IConsumer Consumer => _consumer;
		public IInitialization Init => _registerer;

		// Internal Methods
		internal void FireEventPublished(EventMessage eventMessage)
		{
			if (OnEventPublished != null)
			{
				OnEventPublished(this, eventMessage);
			}
		}
		internal void Unsubscribe(EventHandler<EventMessage> eventHandler)
		{
			OnEventPublished -= eventHandler;
		}
		internal void UnsubscribeAll()//todo:
		{
			foreach (var @delegate in OnEventPublished.GetInvocationList())
			{
				OnEventPublished -= (EventHandler<EventMessage>)@delegate;
			}
		}
		internal void LogError(Exception exception)
		{
			_registeredLogErrorAction?.Invoke(exception);
		}

		// Constructor
		private EventBus()
		{
			_publisher = new Publisher(this);
			_consumer = new Consumer(this);
			_registerer = new Initialization(this);
		}
	}
}


