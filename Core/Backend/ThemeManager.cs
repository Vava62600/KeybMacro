using Avalonia.Styling;
using Avalonia.Markup.Xaml;
using System.Reflection;

public static class ThemeManager
{
    public static void ApplyTheme(string themeName, string paletteName)
    {
        if (!Themes.ContainsKey(themeName))
            themeName = "DefaultLight";

        if (!Palettes.ContainsKey(paletteName))
            paletteName = "Blue";

        CurrentThemeName = themeName;
        CurrentPaletteName = paletteName;

        var theme = Themes[themeName];
        var accentColor = Palettes[paletteName];

        CurrentThemeColors = theme;

        var app = Application.Current;
        if (app == null)
            return;

        // Appliquer les couleurs
        app.Resources["BackgroundBrush"] = new SolidColorBrush(ConvertFromHex(theme.Background));
        app.Resources["ForegroundBrush"] = new SolidColorBrush(ConvertFromHex(theme.Foreground));
        app.Resources["ControlBackgroundBrush"] = new SolidColorBrush(ConvertFromHex(theme.ControlBackground));
        app.Resources["ControlBorderBrush"] = new SolidColorBrush(ConvertFromHex(theme.ControlBorder));
        app.Resources["AccentBrush"] = new SolidColorBrush(ConvertFromHex(accentColor));
        app.Resources["AccentBrushHover"] = new SolidColorBrush(DarkenColor(ConvertFromHex(accentColor), 0.2));
        app.Resources["AccentBrushDisabled"] = new SolidColorBrush(AdjustAlpha(ConvertFromHex(accentColor), 0.5));

        // Charger le style spécifique à appliquer (Win95, Win11, Linux, MacOS, Default)
        string styleType = "Default";
        var themeDict = theme as System.Text.Json.JsonElement?;
        // Ou directement un champ dans ThemeColors (à rajouter)
        // Pour simplifier, on peut faire :
        if (theme is IDictionary<string, object> dict && dict.ContainsKey("StyleType"))
        {
            styleType = dict["StyleType"].ToString() ?? "Default";
        }
        else if (TryGetStyleTypeFromTheme(themeName, out var st))
        {
            styleType = st;
        }

        // Charger et appliquer la ressource axaml du style
        LoadAndApplyStyle(app, styleType);
    }

    private static bool TryGetStyleTypeFromTheme(string themeName, out string styleType)
    {
        styleType = "Default";
        string themeFile = Path.Combine(ThemesFolder, themeName + ".json");
        if (!File.Exists(themeFile)) return false;

        try
        {
            var json = File.ReadAllText(themeFile);
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("StyleType", out var prop))
            {
                styleType = prop.GetString() ?? "Default";
                return true;
            }
        }
        catch { }
        return false;
    }

    private static void LoadAndApplyStyle(Application app, string styleType)
    {
        // Retirer d'abord les styles précédents (Win95, Win11, etc.)
        var keysToRemove = new List<IStyle>();
        foreach (var style in app.Styles)
        {
            if (style is StyleInclude si && si.Source?.OriginalString?.Contains("Styles/") == true)
            {
                keysToRemove.Add(style);
            }
        }
        foreach (var style in keysToRemove)
        {
            app.Styles.Remove(style);
        }

        string styleFileName = styleType switch
        {
            "Windows 95 Theme" => "Win95Styles.axaml",
            "Windown 11 Theme" => "Win11Styles.axaml",
            "Linux Theme" => "LinuxStyles.axaml",
            "MacOS Theme" => "MacOSStyles.axaml",
            _ => "DefaultStyles.axaml"
        };

        var uri = new Uri($"avares://KeybMacro/UI/Styles/{styleFileName}");
        var styleInclude = new StyleInclude(uri)
        {
            Source = uri
        };
        app.Styles.Add(styleInclude);
    }
}
