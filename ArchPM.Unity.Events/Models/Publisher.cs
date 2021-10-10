namespace ArchPM.Unity.Events.Models
{
	internal class Publisher : IPublisher
	{
		private readonly EventBus _eventBus;

		public Publisher(EventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public IPublishResult Publish(IEvent @event)
		{
			_eventBus.FireEventPublished(new EventMessage()
			{
				Event = @event
			});

			return PublishResult.NewResult(@event);
		}
	}
}
