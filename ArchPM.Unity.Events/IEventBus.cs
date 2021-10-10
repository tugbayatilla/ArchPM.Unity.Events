namespace ArchPM.Unity.Events
{
	public interface IEventBus
	{
		IPublisher Publisher { get; }
		IConsumer Consumer { get; }
		IInitialization Init { get; }

	}
}
