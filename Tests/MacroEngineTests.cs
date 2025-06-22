using Xunit;
using KeybMacro.Core.MacroEngine;

namespace KeybMacro.Tests
{
    public class MacroEngineTests
    {
        [Fact]
        public void ActionTypeEnum_ContainsExpectedValues()
        {
            Assert.Contains(ActionType.KeyPress.ToString(), nameof(ActionType.KeyPress));
            Assert.Contains(ActionType.KeyRelease.ToString(), nameof(ActionType.KeyRelease));
            Assert.Contains(ActionType.MouseClick.ToString(), nameof(ActionType.MouseClick));
        }
    }
}