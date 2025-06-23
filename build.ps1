$ErrorActionPreference = "Stop"

# Variables
$buildDir = "Build"
$buildFile = "buildver.txt"
$today = Get-Date -Format "dd-MM-yyyy"

# Fonction d'installation conditionnelle
function Ask-Install {
    param (
        [string]$name,
        [string]$check,
        [scriptblock]$installCmd
    )
    Write-Host "🔍 Vérification de $name..."
    if (-not (Get-Command $check -ErrorAction SilentlyContinue)) {
        $resp = Read-Host "$name n'est pas installé. Voulez-vous l'installer ? (Y/O/N)"
        if ($resp -match '^[YyOo]$') {
            Write-Host "📦 Installation de $name..."
            & $installCmd
        } else {
            Write-Host "⛔ Installation annulée."
            exit 1
        }
    } else {
        Write-Host "✅ $name est déjà installé."
    }
}

# Prérequis
Ask-Install ".NET SDK 8.0" "dotnet" { winget install --id Microsoft.DotNet.SDK.8 -e }
Ask-Install "Git" "git" { winget install --id Git.Git -e }

# Build requis ?
$buildRequired = $false
if (-not (Test-Path $buildFile)) {
    "$today" | Out-File $buildFile
    $buildRequired = $true
} elseif ((Get-Content $buildFile) -ne $today) {
    $buildRequired = $true
}

if (-not (Test-Path "$buildDir/KeybMacro.exe") -or -not (Test-Path "$buildDir/KeybMacro.Core.exe") -or -not (Test-Path "$buildDir/KeybMacro.Tray.exe")) {
    $buildRequired = $true
}

if ($buildRequired) {
    Write-Host "🔨 Compilation..."
    if (-not (dotnet build KeybMacro.sln -c Release)) {
        Write-Host "❌ La compilation .NET a échoué."
        exit 1
    }
    mkdir $buildDir -Force
    Copy-Item Launcher\bin\Release\net8.0\KeybMacro.exe $buildDir -Force
    Copy-Item Core\bin\Release\net8.0\KeybMacro.Core.exe $buildDir -Force
    Copy-Item Tray\bin\Release\net8.0\KeybMacro.Tray.exe $buildDir -Force
    "$today" | Out-File $buildFile -Force
}

# Inno Setup
if (-not (Get-Command ISCC -ErrorAction SilentlyContinue)) {
    Write-Host "⚠️ Inno Setup (ISCC) introuvable. Installez-le pour continuer."
    exit 1
} else {
    Write-Host "✅ Inno Setup disponible."
}

Write-Host "✅ Build Windows terminé."