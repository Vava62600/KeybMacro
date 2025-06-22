using Xunit;
using KeybMacro.Core.PluginSystem;

namespace KeybMacro.Tests
{
    public class PluginSystemTests
    {
        private class DummyPlugin : IPlugin
        {
            public string Name => "Dummy";
            public string Version => "1.0";
            public string Author => "Test";
            public bool HasGui => false;
            public bool HasCli => false;
            public void ExecuteCommandLine(string[] args) { }
            public Avalonia.Controls.UserControl? GetConfigurationUI() => null;
            public void OnLoad() { }
            public void OnUnload() { }
        }

        [Fact]
        public void PluginInterface_CanLoadBasicData()
        {
            var plugin = new DummyPlugin();
            Assert.Equal("Dummy", plugin.Name);
            Assert.Equal("1.0", plugin.Version);
            Assert.Equal("Test", plugin.Author);
        }
    }
}