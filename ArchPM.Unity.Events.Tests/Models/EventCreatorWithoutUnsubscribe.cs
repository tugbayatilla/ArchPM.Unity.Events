namespace ArchPM.Unity.Events.Tests
{
	public partial class Tests
	{
		public class EventCreatorWithoutUnsubscribe
		{
			public EventCreatorWithoutUnsubscribe()
			{
				var eventConsumer1 = EventBus.Instance.Consumer.Consume<Event1>(p => { });
			}
		}


	}
}