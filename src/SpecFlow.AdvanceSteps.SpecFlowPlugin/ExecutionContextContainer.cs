using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.AdvanceSteps
{
    internal static class ExecutionContextContainer
    {
        public static ConcurrentDictionary<int, ExecutionContext> Contexts { get; set; }
                                = new ConcurrentDictionary<int, ExecutionContext>();
    }
}