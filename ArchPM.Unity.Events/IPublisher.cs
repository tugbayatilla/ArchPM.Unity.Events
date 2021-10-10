namespace ArchPM.Unity.Events
{
	public interface IPublisher
	{
		IPublishResult Publish(IEvent @event);
	}
}
