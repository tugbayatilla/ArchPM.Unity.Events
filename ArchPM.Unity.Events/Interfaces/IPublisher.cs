namespace ArchPM.Unity.Events
{
	/// <summary>
	/// 
	/// </summary>
	public interface IPublisher
	{
		/// <summary>
		/// Publishes the specified event.
		/// </summary>
		/// <param name="event">The event.</param>
		/// <returns></returns>
		IPublishResult Publish(IEvent @event);
	}
}
