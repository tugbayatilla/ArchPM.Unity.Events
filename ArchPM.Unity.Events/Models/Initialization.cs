using System;

namespace ArchPM.Unity.Events.Models
{
	internal class Initialization : IInitialization
	{
		private readonly EventBus _eventBus;

		public Initialization(EventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public void RegisterInfoLogAction(Action<string> logAction)
		{
			_eventBus._registeredLogInfoAction = logAction;
		}

		public void RegisterErrorLogAction(Action<Exception> logExceptionAction)
		{
			_eventBus._registeredLogErrorAction = logExceptionAction;
		}
	}
}
