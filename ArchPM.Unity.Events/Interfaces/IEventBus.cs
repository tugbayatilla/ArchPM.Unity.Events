namespace ArchPM.Unity.Events
{
	/// <summary>
	/// 
	/// </summary>
	public interface IEventBus
	{
		/// <summary>
		/// Gets the publisher.
		/// </summary>
		/// <value>
		/// The publisher.
		/// </value>
		IPublisher Publisher { get; }
		
		/// <summary>
		/// Gets the consumer.
		/// </summary>
		/// <value>
		/// The consumer.
		/// </value>
		IConsumer Consumer { get; }

		/// <summary>
		/// Gets the initialize.
		/// </summary>
		/// <value>
		/// The initialize.
		/// </value>
		IInitialization Init { get; }

	}
}
