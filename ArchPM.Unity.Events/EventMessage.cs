using System;

namespace ArchPM.Unity.Events
{
	public class EventMessage
	{
		public IEvent Event { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public Type EventType => Event.GetType();
	}

	public class EventMessage<T> where T : IEvent
	{
		public T Event { get; set; }
		public DateTime CreationTime { get; set; } = DateTime.Now;
		public Type EventType => typeof(T);

		public static EventMessage<T> Convert(EventMessage message)
		{
			return new EventMessage<T>()
			{
				Event = (T)message.Event,
				CreationTime = message.CreatedAt
			};
		}
	}
}
