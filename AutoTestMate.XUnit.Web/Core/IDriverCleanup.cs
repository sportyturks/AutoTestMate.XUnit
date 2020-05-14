﻿namespace AutoTestMate.XUnit.Web.Core
{
	public interface IDriverCleanup
	{
		void Initialise();
		void Dispose();

		IProcess Process { get; }
	}
}