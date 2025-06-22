[Setup]
; Paramètres de l'application
AppName=KeybMacro
AppVersion=1.0.0
DefaultDirName=C:\ProgramData\KeybMacro
DefaultGroupName=KeybMacro
DisableProgramGroupPage=no
OutputDir=.
OutputBaseFilename=KeybMacroSetup
Compression=lzma
SolidCompression=yes
SetupIconFile=KeybMacro.ico
; Ces options permettent d'activer le mode maintenance (Modifier / Réparer)
AllowCancelDuringInstall=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
; Fichiers à la racine (exclus : KBMInstaller.iss, KeybMacroSetup.exe et licence.txt)
Source: "KeybMacro.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D3Dcompiler_47.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "libgcc_s_seh-1.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "libstdc++-6.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "libwinpthread-1.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "main.o"; DestDir: "{app}"; Flags: ignoreversion
Source: "opengl32sw.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "qrc_resources.cpp"; DestDir: "{app}"; Flags: ignoreversion
Source: "qrc_resources.o"; DestDir: "{app}"; Flags: ignoreversion
Source: "Qt6Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Qt6Gui.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Qt6Network.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Qt6Svg.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Qt6Widgets.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "KeybMacro.ico"; DestDir: "{app}"; Flags: ignoreversion

; Inclusion des dossiers et de leur contenu
Source: "generic\*"; DestDir: "{app}\generic"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "iconengines\*"; DestDir: "{app}\iconengines"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "imageformats\*"; DestDir: "{app}\imageformats"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "networkinformation\*"; DestDir: "{app}\networkinformation"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "platforms\*"; DestDir: "{app}\platforms"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "styles\*"; DestDir: "{app}\styles"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "tls\*"; DestDir: "{app}\tls"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "translations\*"; DestDir: "{app}\translations"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\KeybMacro"; Filename: "{app}\KeybMacro.exe"; IconFilename: "{app}\KeybMacro.ico"
Name: "{commondesktop}\KeybMacro"; Filename: "{app}\KeybMacro.exe"; Tasks: desktopicon; IconFilename: "{app}\KeybMacro.ico"

[Tasks]
Name: "desktopicon"; Description: "Créer une icône sur le bureau"; GroupDescription: "Options supplémentaires:"; Flags: unchecked

[Run]
Filename: "{app}\KeybMacro.exe"; Description: "Lancer KeybMacro"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
; Supprime le répertoire d'installation s'il est vide après désinstallation
Type: dirifempty; Name: "{app}"
