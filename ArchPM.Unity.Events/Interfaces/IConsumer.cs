using System;

namespace ArchPM.Unity.Events
{
	/// <summary>
	///   
	/// </summary>
	public interface IConsumer
	{
		/// <summary>
		/// Gets all currently active subscribed Consumers
		/// </summary>
		/// <value>
		/// The active count.
		/// </value>
		int ActiveCount { get; }

		/// <summary>
		/// Subsribe to the event according to T
		/// </summary>
		/// <typeparam name="T">a class that implements IEvent</typeparam>
		/// <param name="action">The action.</param>
		/// <returns>
		/// ICnsumerResult
		/// </returns>
		IConsumeResult Consume<T>(Action<EventMessage<T>> action) where T : IEvent;

		/// <summary>
		/// Consumes the specified action.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="expectedTypes">The expected types.</param>
		/// <returns></returns>
		IConsumeResult Consume(Action<EventMessage> action, params Type[] expectedTypes);
		
		/// <summary>
		/// Unsubscribes all.
		/// </summary>
		void UnsubscribeAll();
	}
}
