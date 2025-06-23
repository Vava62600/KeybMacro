; Inno Setup Script - KeybMacro Installer
#define MyAppName "KeybMacro"
#define MyAppVersion "2.0.0"
#define MyAppPublisher "Vava62600"
#define MyAppURL "https://github.com/vava62600/KeybMacro"
#define MyAppExeName "KeybMacro.exe"

[Setup]
AppId={{8E41AB1F-5F9B-4266-A7A7-KEYBMACRO}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
DefaultDirName={autopf}\KeybMacro
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=Assets\License\license.txt
OutputDir=Installer
OutputBaseFilename=KeybMacro Setup Version {#MyAppVersion}
Compression=lzma2
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "fr"; MessagesFile: "compiler:Languages\French.isl"
Name: "en"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "autostart"; Description: "Lancer KeybMacro au démarrage de Windows"; Flags: unchecked
Name: "launchreadme"; Description: "Afficher le fichier README après l'installation"; Flags: unchecked
Name: "theme_light"; Description: "Thème clair"; GroupDescription: "Choix du thème"
Name: "theme_dark"; Description: "Thème sombre"; GroupDescription: "Choix du thème"

[Files]
Source: "Build\Release\KeybMacro.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "Assets\License\license_fr.txt"; DestDir: "{app}"
Source: "Assets\Themes\Palettes\*.json"; DestDir: "{app}\Assets\Themes\Palettes"
Source: "README.md"; DestDir: "{app}"; Flags: isreadme
Source: "Assets\Icons\icon.png"; DestDir: "{app}"; Flags: ignoreversion
Source: "Assets\Icons\icon_dark.png"; DestDir: "{app}"; Flags: ignoreversion
Source: "Assets\Icons\icon_light.png"; DestDir: "{app}"; Flags  : ignoreversion
Source: "Assets\Icons\icon.svg"; DestDir: "{app}"; Flags: ignoreversion


[Icons]
Name: "{group}\KeybMacro"; Filename: "{app}\KeybMacro.exe"
Name: "{group}\Désinstaller KeybMacro"; Filename: "{uninstallexe}"
Name: "{commondesktop}\KeybMacro"; Filename: "{app}\KeybMacro.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\KeybMacro.exe"; Description: "Lancer KeybMacro"; Flags: nowait postinstall skipifsilent

[Registry]
; AutoStart if checked
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; \
    ValueType: string; ValueName: "KeybMacro"; ValueData: """{app}\KeybMacro.exe"""; \
    Flags: uninsdeletevalue; Tasks: autostart

[Code]
function IsDotNetInstalled(): Boolean;
begin
  Result := RegKeyExists(HKLM, 'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost');
end;

procedure InstallDotNet();
var
  DotNetInstaller: string;
begin
  DotNetInstaller := ExpandConstant('{tmp}\dotnet-install.exe');
  DownloadTemporaryFile('https://download.visualstudio.microsoft.com/download/pr/2f3e3e64-8d63-4380-8197-d8c041e42d2c/83e0e75ef2ed6c147b7871c17df62634/dotnet-sdk-8.0.4-win-x64.exe', DotNetInstaller);
  ShellExec('', DotNetInstaller, '/install /quiet /norestart', '', SW_HIDE, ewWaitUntilTerminated, nil);
end;

function InitializeSetup(): Boolean;
begin
  if not IsDotNetInstalled() then begin
    MsgBox('Le runtime .NET 8.0.4 est requis. Il va maintenant être installé.', mbInformation, MB_OK);
    InstallDotNet();
  end;
  Result := True;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  ConfigFile: string;
  JSON: string;
begin
  if CurStep = ssPostInstall then begin
    ConfigFile := ExpandConstant('{app}\UserSettings.json');

    JSON :=
      '{' + #13#10 +
      '  "Theme": "' + (WizardIsTaskSelected('theme_dark') ? 'Dark' : 'Light') + '",' + #13#10 +
      '  "Language": "' + ActiveLanguage + '",' + #13#10 +
      '  "Startup": ' + BoolToStr(WizardIsTaskSelected('autostart'), True) + #13#10 +
      '}';

    SaveStringToFile(ConfigFile, JSON, False);

    if WizardIsTaskSelected('launchreadme') then
      ShellExec('', ExpandConstant('{app}\README.md'), '', '', SW_SHOWNORMAL, ewNoWait, nil);
  end;
end;
