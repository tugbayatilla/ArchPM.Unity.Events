using System;

namespace ArchPM.Unity.Events
{
	/// <summary>
	/// 
	/// </summary>
	public interface ILogRegister
	{
		/// <summary>
		/// Registers the information log action.
		/// </summary>
		/// <param name="logAction">The log action.</param>
		void RegisterInfoLogAction(Action<string> logAction);

		/// <summary>
		/// Registers the error log action.
		/// </summary>
		/// <param name="logAction">The log action.</param>
		void RegisterErrorLogAction(Action<Exception> logAction);
	}
}
