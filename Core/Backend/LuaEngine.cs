using System;
using NLua;

namespace KeybMacro.Core.Backend
{
    public class LuaEngine : IDisposable
    {
        private readonly Lua _lua;

        public LuaEngine()
        {
            _lua = new Lua();
            // Ici tu peux enregistrer des fonctions C# exposées au Lua
        }

        public void DoString(string luaCode)
        {
            _lua.DoString(luaCode);
        }

        public object[] DoFile(string filename)
        {
            return _lua.DoFile(filename);
        }

        public void Dispose()
        {
            _lua.Dispose();
        }
    }
}
