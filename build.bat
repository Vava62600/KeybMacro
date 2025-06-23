@echo off
setlocal EnableDelayedExpansion

set BUILD_DIR=Build
set BUILD_FILE=buildver.txt
for /f %%i in ('powershell -Command "Get-Date -Format \"dd-MM-yyyy\""') do set TODAY=%%i

:: --- Fonction installation
:ask_install
set NAME=%1
set CHECK=%2
set INSTALL=%3
where %CHECK% >nul 2>&1
if errorlevel 1 (
    set /p yn="%NAME% n'est pas installé. Installer ? (Y/O/N) "
    if /i "!yn!"=="Y" goto do_install
    if /i "!yn!"=="O" goto do_install
    echo Installation annulée.
    exit /b 1
) else (
    echo %NAME% déjà installé.
    goto:eof
)
:do_install
powershell -Command "%INSTALL%"
goto:eof

call :ask_install ".NET SDK 8.0" dotnet "winget install --id Microsoft.DotNet.SDK.8 -e"
call :ask_install "Git" git "winget install --id Git.Git -e"

:: Check build
set BUILD_REQUIRED=false
if not exist %BUILD_FILE% (
    echo %TODAY% > %BUILD_FILE%
    set BUILD_REQUIRED=true
)
for /f %%x in (%BUILD_FILE%) do set BUILD_DATE=%%x
if not "%BUILD_DATE%"=="%TODAY%" set BUILD_REQUIRED=true

if not exist %BUILD_DIR%\KeybMacro.exe set BUILD_REQUIRED=true
if not exist %BUILD_DIR%\KeybMacro.Core.exe set BUILD_REQUIRED=true
if not exist %BUILD_DIR%\KeybMacro.Tray.exe set BUILD_REQUIRED=true

if "%BUILD_REQUIRED%"=="true" (
    echo Compilation...
    dotnet build KeybMacro.sln -c Release || exit /b 1
    mkdir %BUILD_DIR%
    copy /Y Launcher\bin\Release\net8.0\KeybMacro.exe %BUILD_DIR%\
    copy /Y Core\bin\Release\net8.0\KeybMacro.Core.exe %BUILD_DIR%\
    copy /Y Tray\bin\Release\net8.0\KeybMacro.Tray.exe %BUILD_DIR%\
    echo %TODAY% > %BUILD_FILE%
)

where ISCC >nul 2>&1
if errorlevel 1 (
    echo Inno Setup introuvable. Veuillez l'installer.
    exit /b 1
)

echo Build Windows terminé.
