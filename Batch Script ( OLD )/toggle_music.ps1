# Créer une fonction pour envoyer une touche Media Play/Pause à Windows
function Send-PlayPauseKey {
    Add-Type -TypeDefinition @"
    using System;
    using System.Runtime.InteropServices;
    public class Keyboard {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
    }
"@

    # Code de la touche Media Play/Pause
    $VK_MEDIA_PLAY_PAUSE = 0xB3

    # Simuler la pression de la touche Play/Pause
    [Keyboard]::keybd_event($VK_MEDIA_PLAY_PAUSE, 0, 0, 0)  # Touche pressée
    [Keyboard]::keybd_event($VK_MEDIA_PLAY_PAUSE, 0, 2, 0)  # Touche relâchée
}

# Vérifiez si la musique est en pause ou en lecture (en fonction de votre lecteur, si ce n'est pas géré par un autre programme, la lecture ou pause se fait automatiquement via les raccourcis système).
Send-PlayPauseKey
Write-Host "Basculement entre pause et reprise."
