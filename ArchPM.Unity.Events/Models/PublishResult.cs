namespace ArchPM.Unity.Events.Models
{
	internal class PublishResult : IPublishResult
	{
		public IEvent Event { get; set; }

		public static IPublishResult NewResult(IEvent @event) => new PublishResult() { Event = @event };
	}
}
