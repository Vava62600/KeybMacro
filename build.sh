#!/bin/bash
set -e

# --- Variables ---
BUILD_DIR="Build"
BUILD_FILE="buildver.txt"
TODAY=$(date +"%d-%m-%Y")

# --- Fonction d'installation conditionnelle ---
ask_install() {
  local name="$1"
  local check_cmd="$2"
  local install_cmd="$3"

  echo "🔍 Vérification de $name..."
  if ! command -v $check_cmd &> /dev/null; then
    read -p "$name n'est pas installé. Voulez-vous l'installer ? (Y/O pour oui, N pour non) " yn
    case $yn in
      [YyOo]*)
        echo "📦 Installation de $name..."
        eval $install_cmd
        ;;
      *)
        echo "⛔ Installation annulée."
        exit 1
        ;;
    esac
  else
    echo "✅ $name est déjà installé."
  fi
}

# --- Vérifications des outils nécessaires ---
ask_install ".NET SDK 8.0" "dotnet" "sudo apt install -y dotnet-sdk-8.0"
ask_install "Git" "git" "sudo apt install -y git"

# --- Compilation nécessaire ? ---
build_required=false
if [[ ! -f "$BUILD_FILE" ]]; then
  echo "📄 Création de $BUILD_FILE avec la date d'aujourd'hui."
  echo "$TODAY" > "$BUILD_FILE"
  build_required=true
elif [[ $(cat "$BUILD_FILE") != "$TODAY" ]]; then
  echo "📅 Build obsolète. Recompilation requise."
  build_required=true
fi

if [[ ! -f "$BUILD_DIR/KeybMacro" || ! -f "$BUILD_DIR/KeybMacro.Core" || ! -f "$BUILD_DIR/KeybMacro.Tray" ]]; then
  echo "🔧 Exécutables manquants. Compilation requise."
  build_required=true
fi

if [ "$build_required" = true ]; then
  echo "🔨 Compilation du projet..."
  if ! dotnet build KeybMacro.sln -c Release; then
    echo "❌ La compilation .NET a échoué."
    exit 1
  fi
  mkdir -p "$BUILD_DIR"
  cp Launcher/bin/Release/net8.0/KeybMacro "$BUILD_DIR/KeybMacro"
  cp Core/bin/Release/net8.0/KeybMacro.Core "$BUILD_DIR/KeybMacro.Core"
  cp Tray/bin/Release/net8.0/KeybMacro.Tray "$BUILD_DIR/KeybMacro.Tray"
  echo "$TODAY" > "$BUILD_FILE"
fi

# --- Vérification de Inno Setup sous Windows ---
if [[ "$(uname -s)" == MINGW* || "$(uname -s)" == CYGWIN* || "$(uname -s)" == MSYS* ]]; then
  echo "🖥️ Environnement Windows détecté. Vérification d'Inno Setup..."
  if ! command -v ISCC &> /dev/null; then
    echo "⚠️ Inno Setup (ISCC) n'est pas installé. Veuillez l'installer pour continuer."
    exit 1
  fi
  echo "✅ Inno Setup est disponible."
fi

# --- Génération d’un .tar.gz (ou DEB plus tard) ---
echo "📦 Création de l'archive du build..."
tar czvf KeybMacro-linux-$TODAY.tar.gz -C "$BUILD_DIR" .

echo "✅ Build terminé avec succès."
