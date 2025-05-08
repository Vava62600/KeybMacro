#include <windows.h>
#include <winuser.h>
#ifndef KEYEVENTF_KEYDOWN
#define KEYEVENTF_KEYDOWN 0x0000
#endif
#ifndef KEYEVENTF_KEYUP
#define KEYEVENTF_KEYUP 0x0002
#endif
#include <string>
#include <thread>
#include <chrono>
#include <conio.h>
#include <QApplication>
#include <QSystemTrayIcon>
#include <QMenu>
#include <QAction>
#include <QIcon>
#include <QSettings>
#include <QPalette>
#include <QShortcut>
#include <QMessageBox>
#include <QProcess>
#include <QTimer>
#include <QWidget>
#include <QVBoxLayout>
#include <QTextBrowser>
#include <QLabel>
#include <QDialog>
#include <QHBoxLayout>
#include <QPushButton>
#include <QPixmap>
#include <QDesktopServices>
#include <QUrl>
#include <QComboBox>
#include <QSettings>
#include <QStyleFactory>
#include <QCloseEvent>  // Ajoutez cette ligne

class ShowHelp : public QWidget {
public:
    ShowHelp() {
        setWindowTitle("Help | HotKeyMacro");
        setFixedSize(1141, 536);

        QVBoxLayout* layout = new QVBoxLayout(this);

        QTextBrowser* textBrowser = new QTextBrowser(this);
        textBrowser->setReadOnly(true);

        // Style PowerShell
        QString css = R"(
            background-color: black;
            color: white;
            font-family: Consolas, 'Courier New', monospace;
            font-size: 14px;
            padding: 10px;
        )";
        textBrowser->setStyleSheet(css);

        // Texte formaté avec un look de terminal PowerShell
        QString text = R"(
            <pre>
Function List:
=======================
CTRL + ALT + F1 = Mute
CTRL + ALT + F2 = Volume Down
CTRL + ALT + F3 = Volume Up
CTRL + ALT + F4 = Play/Pause Track
CTRL + ALT + F6 = Previous Track
CTRL + ALT + F7 = Next Track
CTRL + ALT + F9 = Start Stream Setup (OBS, Twitch, Discord)
CTRL + ALT + H = Help
CTRL + ALT + Q = Quit

For more information please visit https://github.com/vava62600/HotKeyMacro/
            </pre>
        )";

        textBrowser->setHtml(text);
        layout->addWidget(textBrowser);
    }

protected:
    void closeEvent(QCloseEvent *event) override {
        event->ignore();
        this->hide();
    }
};

class LibrariesDialog : public QDialog {
public:
    LibrariesDialog(QWidget *parent = nullptr) : QDialog(parent) {
        setWindowTitle("Libraries");
        setFixedSize(400, 600);

        QVBoxLayout* layout = new QVBoxLayout(this);

        QLabel* libsLabel = new QLabel("<h3>Libraries Used:</h3>", this);
        libsLabel->setAlignment(Qt::AlignLeft);
        layout->addWidget(libsLabel);

        // Windows Libraries
        QLabel* windowsLibs = new QLabel("<b>Windows Libraries:</b><br>"
                                         "windows.h<br>"
                                         "winuser.h<br>"
                                         "conio.h<br>"
                                         "thread<br>"
                                         "chrono<br>", this);
        windowsLibs->setAlignment(Qt::AlignLeft);
        layout->addWidget(windowsLibs);

        // Qt Libraries
        QLabel* qtLibs = new QLabel("<b>Qt Libraries:</b><br>"
                                    "QApplication<br>"
                                    "QSystemTrayIcon<br>"
                                    "QMenu<br>"
                                    "QAction<br>"
                                    "QIcon<br>"
                                    "QSettings<br>"
                                    "QPalette<br>"
                                    "QShortcut<br>"
                                    "QMessageBox<br>"
                                    "QProcess<br>"
                                    "QTimer<br>"
                                    "QWidget<br>"
                                    "QVBoxLayout<br>"
                                    "QTextBrowser<br>"
                                    "QLabel<br>"
                                    "QDialog<br>"
                                    "QHBoxLayout<br>"
                                    "QPushButton<br>"
                                    "QPixmap<br>"
                                    "QDesktopServices<br>"
                                    "QUrl<br>", this);
        qtLibs->setAlignment(Qt::AlignLeft);
        layout->addWidget(qtLibs);
    }

protected:
    void closeEvent(QCloseEvent *event) override {
        event->ignore();
        this->hide();
    }
};

class SettingsDialog : public QDialog {
public:
    SettingsDialog(QWidget *parent = nullptr) : QDialog(parent) {
        setWindowTitle("Settings");
        setFixedSize(400, 300);
        setWindowFlags(windowFlags() & ~Qt::WindowContextHelpButtonHint);  // Désactive le bouton d'aide

        QVBoxLayout* layout = new QVBoxLayout(this);

        // ComboBox pour la sélection de la langue
        QComboBox* languageComboBox = new QComboBox(this);
        languageComboBox->addItem("Français", "fr");
        languageComboBox->addItem("Deutsch", "de");
        languageComboBox->addItem("Español", "es");
        languageComboBox->addItem("English", "en");
        languageComboBox->addItem("Italiano", "it");
        connect(languageComboBox, &QComboBox::currentIndexChanged, this, &SettingsDialog::changeLanguage);
        layout->addWidget(languageComboBox);

        // Bouton pour basculer entre le thème clair et sombre
        QPushButton* themeButton = new QPushButton("Toggle Theme", this);
        connect(themeButton, &QPushButton::clicked, this, &SettingsDialog::toggleTheme);
        layout->addWidget(themeButton);

        // Ajouter un bouton "Fermer"
        QPushButton* closeButton = new QPushButton("Close", this);
        connect(closeButton, &QPushButton::clicked, this, &QDialog::accept);
        layout->addWidget(closeButton);
    }

private slots:
    void changeLanguage(int index) {
        QString selectedLanguage = languageComboBox->itemData(index).toString();
        // Charger les paramètres linguistiques en fonction de la langue sélectionnée
        if (selectedLanguage == "fr") {
            // Charger traduction pour le français
        } else if (selectedLanguage == "de") {
            // Charger traduction pour l'allemand
        } else if (selectedLanguage == "es") {
            // Charger traduction pour l'espagnol
        } else if (selectedLanguage == "en") {
            // Charger traduction pour l'anglais
        } else if (selectedLanguage == "it") {
            // Charger traduction pour l'italien
        }
        // Appliquer les changements de langue
    }

    void toggleTheme() {
        // Changez le thème entre clair et sombre
        QString currentStyle = qApp->style()->objectName();
        if (currentStyle == "Fusion") {
            // Passer au thème sombre
            QApplication::setStyle(QStyleFactory::create("Fusion"));
            qApp->setPalette(QPalette(QPalette::Dark));
        } else {
            // Passer au thème clair
            QApplication::setStyle(QStyleFactory::create("Fusion"));
            qApp->setPalette(QPalette(QPalette::Light));
        }
    }

private:
    QComboBox* languageComboBox;

protected:
    void closeEvent(QCloseEvent *event) override {
        event->ignore();
        this->hide();
    }
};

class AboutDialog : public QDialog {
public:
    AboutDialog(QWidget *parent = nullptr) : QDialog(parent) {
        setWindowTitle("About - KeybMacro");
        setFixedSize(400, 300);

        QVBoxLayout* layout = new QVBoxLayout(this);

        QLabel* title = new QLabel("<h1>KeybMacro</h1>", this);
        title->setAlignment(Qt::AlignCenter);
        layout->addWidget(title);

        QLabel* version = new QLabel("<p><b>Version:</b> 1.0.1</p>", this);
        version->setAlignment(Qt::AlignCenter);
        layout->addWidget(version);

        QLabel* description = new QLabel("<p><b>Description:</b> A keyboard macro manager for Windows.</p>", this);
        description->setAlignment(Qt::AlignCenter);
        layout->addWidget(description);

        QLabel* credits = new QLabel("<p><b>Credits:</b> Developed by vava62600.</p>", this);
        credits->setAlignment(Qt::AlignCenter);
        layout->addWidget(credits);

        QLabel* github = new QLabel("<p><b>Website:</b> <a href='https://github.com/vava62600/KeybMacro'>https://github.com/vava62600/KeybMacro</a></p>", this);
        github->setAlignment(Qt::AlignCenter);
        github->setOpenExternalLinks(true);
        layout->addWidget(github);

        QPushButton* librariesButton = new QPushButton("Libraries", this);
        connect(librariesButton, &QPushButton::clicked, this, &AboutDialog::openLibrariesDialog);
        layout->addWidget(librariesButton);

        QPushButton* closeButton = new QPushButton("Close", this);
        connect(closeButton, &QPushButton::clicked, this, &QDialog::reject);
        layout->addWidget(closeButton);
    }

private slots:
    void openLibrariesDialog() {
        LibrariesDialog* librariesDialog = new LibrariesDialog(this);
        librariesDialog->exec();
    }

protected:
    void closeEvent(QCloseEvent *event) override {
        event->ignore();
        this->hide();
    }
};

void showHelp() {
    ShowHelp* helpWindow = new ShowHelp();
    helpWindow->show();
}

void showAbout() {
    AboutDialog* aboutWindow = new AboutDialog();
    aboutWindow->exec();
}

bool isDarkTheme() {
    return QApplication::palette().color(QPalette::Window).lightness() < 128;
}

void setAutoStart() {
    QSettings settings("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run", QSettings::NativeFormat);
    QString appName = "KeybMacro";
    QString appPath = QCoreApplication::applicationFilePath();
    settings.setValue(appName, appPath.replace("/", "\\"));
}

void updateTrayIcon(QSystemTrayIcon &trayIcon) {
    QPalette palette = QApplication::palette();
    bool isDarkTheme = (palette.color(QPalette::Window).lightness() < 128);

    if (isDarkTheme) {
        trayIcon.setIcon(QIcon(":/icons/icon-dark.png"));
    } else {
        trayIcon.setIcon(QIcon(":/icons/icon-light.svg"));
    }
}

void ExecuteCommand(const std::string& command) {
    system(command.c_str());
}

void SendNotification(const std::string& message) {
    std::string command = "powershell -Command \"New-BurntToastNotification -AppLogo 'C:\\Windows\\System32\\SecurityAndMaintenance.png' -Text ';Lancement de l\\'équipement de stream', '" + message + "'\"";
    ExecuteCommand(command);
}

void Mute() {
    keybd_event(VK_VOLUME_MUTE, 0, KEYEVENTF_KEYDOWN, 0);
    keybd_event(VK_VOLUME_MUTE, 0, KEYEVENTF_KEYUP, 0);
}

void VolumeDown() {
    keybd_event(VK_VOLUME_DOWN, 0, KEYEVENTF_KEYDOWN, 0);
    keybd_event(VK_VOLUME_DOWN, 0, KEYEVENTF_KEYUP, 0);
}

void VolumeUp() {
    keybd_event(VK_VOLUME_UP, 0, KEYEVENTF_KEYDOWN, 0);
    keybd_event(VK_VOLUME_UP, 0, KEYEVENTF_KEYUP, 0);
}

void NextTrack() {
    keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_KEYDOWN, 0);
    keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_KEYUP, 0);
}

void PrevTrack() {
    keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_KEYDOWN, 0);
    keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_KEYUP, 0);
}

void ToggleMusic() {
    keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_KEYDOWN, 0);
    keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_KEYUP, 0);
}

void showHelpDialog() {
    QMessageBox::information(nullptr, "Help", "Liste des fonctions :\n...");
}

void StreamSetupStart() {
    SendNotification("Lancement d'OBS, Twitch et Discord");

    ExecuteCommand("start \"\" \"C:\\Users\\Public\\Desktop\\OBS Studio.lnk\"");
    ExecuteCommand("start \"\" \"https://www.twitch.tv/vava62600\"");
    ExecuteCommand("start \"\" \"C:\\Users\\Vava\\AppData\\Local\\Discord\\Update.exe\" --processStart \"Discord.exe\"");
}

void helpShortcut() {
    qDebug() << "Help shortcut activated!";
}

void CheckHotkeys() {
    while (true) {
        if ((GetAsyncKeyState(VK_CONTROL) & 0x8000) && (GetAsyncKeyState(VK_MENU) & 0x8000)) {

            if (GetAsyncKeyState(VK_F1) & 0x8000) {
                Mute();
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
            }
            if (GetAsyncKeyState(VK_F2) & 0x8000) {
                VolumeDown();
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
            }
            if (GetAsyncKeyState(VK_F3) & 0x8000) {
                VolumeUp();
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
            }
            if (GetAsyncKeyState(VK_F5) & 0x8000) {
                ToggleMusic();
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
            }
            if (GetAsyncKeyState(VK_F6) & 0x8000) {
                PrevTrack();
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
            }
            if (GetAsyncKeyState(VK_F7) & 0x8000) {
                NextTrack();
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
            }
            if (GetAsyncKeyState(VK_F9) & 0x8000) {
                StreamSetupStart();
                std::this_thread::sleep_for(std::chrono::milliseconds(500));
            }
        }

        std::this_thread::sleep_for(std::chrono::milliseconds(5));
    }
}

int main(int argc, char *argv[]) {
    QApplication app(argc, argv);

    // Empêcher l'application de quitter lorsque la dernière fenêtre est fermée
    QApplication::setQuitOnLastWindowClosed(false);

    setAutoStart();

    HWND hWnd = GetConsoleWindow();
    ShowWindow(hWnd, SW_HIDE);

    QSystemTrayIcon trayIcon;
    updateTrayIcon(trayIcon);
    trayIcon.setVisible(true);

    QShortcut *helpShortcut = new QShortcut(QKeySequence("Ctrl+Alt+H"), &app);
    QObject::connect(helpShortcut, &QShortcut::activated, []() {
        QMessageBox::information(nullptr, "Help", "Liste des fonctions :\n...");
    });

    QMenu trayMenu;
    QAction *helpAction = new QAction("Help", &trayMenu);
    QObject::connect(helpAction, &QAction::triggered, showHelp);
    trayMenu.addAction(helpAction);
    QAction aboutAction("About", &app);
    QObject::connect(&aboutAction, &QAction::triggered, showAbout);
    trayMenu.addAction(&aboutAction);
    QAction quitAction("Quit", &app);
    QObject::connect(&quitAction, &QAction::triggered, &app, &QApplication::quit);
    trayMenu.addAction(&quitAction);
    trayIcon.setContextMenu(&trayMenu);

    QObject::connect(&trayIcon, &QSystemTrayIcon::activated, [&](QSystemTrayIcon::ActivationReason /*reason*/) {
    });

    if (QSysInfo::productType() == "windows") {
        QApplication::setStyle(QStyleFactory::create("Fusion"));
        qApp->setPalette(QPalette(QPalette::Light));  // Choisir le thème par défaut de Qt
    }

    QTimer timer;
    QObject::connect(&timer, &QTimer::timeout, [&]() {
        updateTrayIcon(trayIcon);
    });
    timer.start(1000);

    std::thread hotkeyThread(CheckHotkeys);
    hotkeyThread.detach();

    QIcon appIcon = trayIcon.icon();
    app.setWindowIcon(appIcon);

    return app.exec();
}
