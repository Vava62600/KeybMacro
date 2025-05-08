@echo off
:: Afficher une notification dans le centre de notifications
New-BurntToastNotification -AppLogo C:\Windows\System32\SecurityAndMaintenance.png -Text "Lancement de l'équipement de stream", 'Lancement d''OBS, Twitch et Discord'

:: Lancer OBS
start "" "D:\Users\Vava\Programmes\OBS\obs-studio\bin\64bit\obs64.exe"

:: Ouvrir Twitch dans le navigateur par défaut
start "" "https://www.twitch.tv/vava62600"

:: Lancer Discord
start "" "C:\Users\Vava\AppData\Local\Discord\Update.exe" --processStart "Discord.exe"

:: Fin du script
exit
