using System;

namespace ArchPM.Unity.Events
{
	public interface IConsumer
	{
		/// <summary>
		/// Gets all currently active subscribed Consumers
		/// </summary>
		int ActiveCount { get; }

		/// <summary>
		/// Subsribe to the event according to T
		/// </summary>
		/// <typeparam name="T">a class that implements IEvent</typeparam>
		/// <param name="action"></param>
		/// <returns>ICnsumerResult</returns>
		IConsumerResult Consume<T>(Action<EventMessage<T>> action) where T : IEvent;
		IConsumerResult Consume(Action<EventMessage> action, params Type[] expectedTypes);
		void UnsubscribeAll();
	}
}
