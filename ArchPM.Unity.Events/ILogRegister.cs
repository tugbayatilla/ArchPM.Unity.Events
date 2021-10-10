using System;

namespace ArchPM.Unity.Events
{
	public interface ILogRegister
	{
		void RegisterInfoLogAction(Action<string> logAction);
		void RegisterErrorLogAction(Action<Exception> logAction);
	}
}
