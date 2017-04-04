using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.AdvanceSteps
{
    public class AdvanceStepsException : Exception
    {
        public AdvanceStepsException(string message) : base(message)
        {
        }
    }
}
