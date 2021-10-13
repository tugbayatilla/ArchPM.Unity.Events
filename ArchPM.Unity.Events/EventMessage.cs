using System;

namespace ArchPM.Unity.Events
{
	/// <summary>
	/// 
	/// </summary>
	public class EventMessage
	{
		/// <summary>
		/// Gets or sets the event.
		/// </summary>
		/// <value>
		/// The event.
		/// </value>
		public IEvent Event { get; set; }
		/// <summary>
		/// Gets or sets the created at.
		/// </summary>
		/// <value>
		/// The created at.
		/// </value>
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		/// <value>
		/// The type of the event.
		/// </value>
		public Type EventType => Event.GetType();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EventMessage<T> where T : IEvent
	{
		/// <summary>
		/// Gets or sets the event.
		/// </summary>
		/// <value>
		/// The event.
		/// </value>
		public T Event { get; set; }
		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>
		/// The creation time.
		/// </value>
		public DateTime CreationTime { get; set; } = DateTime.Now;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		/// <value>
		/// The type of the event.
		/// </value>
		public Type EventType => typeof(T);

		/// <summary>
		/// Converts the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		internal static EventMessage<T> Convert(EventMessage message)
		{
			return new EventMessage<T>()
			{
				Event = (T)message.Event,
				CreationTime = message.CreatedAt
			};
		}
	}
}
