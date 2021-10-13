namespace ArchPM.Unity.Events
{
	/// <summary>
	/// 
	/// </summary>
	public interface IPublishResult
	{
		/// <summary>
		/// Gets or sets the event.
		/// </summary>
		/// <value>
		/// The event.
		/// </value>
		IEvent Event { get; set; }
	}
}
