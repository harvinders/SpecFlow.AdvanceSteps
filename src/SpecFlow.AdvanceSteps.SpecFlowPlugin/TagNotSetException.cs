using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.AdvanceSteps
{
    public class TagNotSetException:Exception
    {
        private readonly string tagName;
        private readonly string methodName;

        public TagNotSetException(string tagName, string methodName)
        {
            this.tagName = tagName;
            this.methodName = methodName;
        }

        public override string ToString()
        {
            return $"Please set '{this.tagName}' tag on the scenario before attempting to call the method '{this.methodName}'";
        }
    }
}
