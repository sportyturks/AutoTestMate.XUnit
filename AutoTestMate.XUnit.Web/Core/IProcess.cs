﻿using System.Collections.Generic;

namespace AutoTestMate.XUnit.Web.Core
{
    public interface IProcess
    {
        IEnumerable<int> GetProcessesByName(string name);
        int GetProcessesById(int id);
        void Kill(int id);
    }
}