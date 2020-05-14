using System;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public static class CurrentTestManager
    {
        private static ITestManager _uniqueInstance;

        private static readonly object SyncLock = new Object();
        public static ITestManager Current {
            get
            {
                lock (SyncLock)
                {
                    return _uniqueInstance;
                }
            }
            set
            {
                lock (SyncLock)
                { 
                    _uniqueInstance = value;
                }
            }
        }
    }
}