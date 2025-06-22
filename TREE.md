KeybMacro/
├── Assets/
│   ├── Icons/
│   ├── Themes/
│   │   ├── Light.json
│   │   ├── Dark.json
│   │   ├── Custom/
│   │   │    ├── MacOS.json
│   │   │    ├── Linux.json
│   │   │    ├── Win11.json
│   │   │    ├── WinXP.json
│   │   │    └── Win95.json
│   │   └── Palettes/
│   │       ├── Blue.json
│   │       ├── Green.json
│   │       ├── Red.json
│   │       ├── Orange.json
│   │       └── Grey.json
│   └── Fonts/
├── Core/
│   ├── MacroEngine/
│   │   ├── MacroManager.cs
│   │   ├── ActionTypes.cs
│   │   └── InputHandler.cs
│   ├── PluginSystem/
│   │   ├── PluginInterface.cs
│   │   ├── PluginManager.cs
│   │   └── PluginSettingsSchema.json
│   ├── Settings/
│   │   ├── UserProfile.cs
│   │   ├── SettingsManager.cs
│   │   └── DefaultSettings.json
│   └── Backend/
│       ├── ThemeManager.cs
│       ├── UpdateManager.cs
│       ├── ScriptBridge.cs
│       └── LuaEngine.cs
├── Scripts/
│   ├── Install/
│   │   └── setup.py
│   ├── Helpers/
│   │   ├── auto_start.py
│   │   ├── config_validator.py
│   │   └── update_fetcher.py
│   └── Lua/
│       ├── default.lua
│       └── sandbox.lua
├── UI/
│   ├── Windows/
│   │   ├── MainWindow.axaml
│   │   └── SettingsWindow.axaml
│   ├── ViewModels/
│   │   ├── MainViewModel.cs
│   │   └── SettingsViewModel.cs
│   ├── Controls/
│   │   ├── MacroList.axaml
│   │   ├── ActionEditor.axaml
│   │   └── ThemeSelector.axaml
│   └── Styles/
│       ├── BaseStyles.axaml
│       └── ThemeBindings.axaml
├── Platform/
│   ├── Windows/
│   │   ├── WinInputHook.cpp
│   │   └── WinInputHookInterop.cs
│   └── Linux/
│       ├── X11InputHook.cpp
│       └── X11InputHookInterop.cs
├── Tests/
│   ├── Core.Tests.csproj
│   ├── PluginSystemTests.cs
│   ├── MacroEngineTests.cs
│   └── UITests.cs
├── App.axaml
├── App.xaml.cs
├── KeybMacro.csproj
└── README.md
