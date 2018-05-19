using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.Internal;
using System.Drawing;
using DevExpress.Web;

public static class Utils {

    internal const string
        CurrentThemeCookieKeyPrefix = "DXCurrentTheme",
        CurrentBaseColorCookieKeyPrefix = "DXCurrentBaseColor",
        TunableThemeCookieKeyPrefix = "DXTunableTheme",
        CurrentFontCookieKey = "DXCurrentFont",
        DefaultThemeName = "Metropolis";

    public static string TunableThemeCookieKey {
        get {
            return TunableThemeCookieKeyPrefix;
        }
    }
    public static string DefaultTheme {
        get {
            return DefaultThemeName;
        }
    }
    public static List<ListEditItem> GetFontFamiliesDataSource() {
        return CustomFonts.Select(f => new ListEditItem() { Text = GetShortFontName(f), Value = f }).ToList();
    }
    public static string GetDefaultBaseColor(string themeName) {
        return ThemesModel.GetThemeModel(themeName).BaseColor;
    }

    public static string CurrentBaseColorCookieKey {
        get {
            return CurrentBaseColorCookieKeyPrefix;
        }
    }

    static HttpContext Context {
        get {
            return HttpContext.Current;
        }
    }
    public static string[] CustomBaseColors {
        get {
            return new string[] {
                    GetOrReplaceCurrentThemeDefaultBaseColor(),
                    "#4796CE",
                    "#35B86B",
                    "#CE5B47",
                    "#F7A233",
                    "#9F47CE",
                    "#5C57C9",
                    "#CE4776",
                };
        }
    }
    static Dictionary<string, string> ThemeBases = new Dictionary<string, string>() {
            { "MaterialCompact", "Material" }
        };
    public static bool CanChangeTheme {
        get {
            return true; ;
        }
    }
    static HttpRequest Request {
        get {
            return Context.Request;
        }
    }
    public static string CurrentThemeCookieKey {
        get {
            return CurrentThemeCookieKeyPrefix;
        }
    }
    public static string CurrentTheme {
        get {
            if(CanChangeTheme && Request.Cookies[CurrentThemeCookieKey] != null)
                return HttpUtility.UrlDecode(Request.Cookies[CurrentThemeCookieKey].Value);
            return DefaultTheme;
        }
    }
    public static string GetOrReplaceCurrentThemeDefaultBaseColor() {
        if(ThemeBases.ContainsKey(CurrentTheme)) {
            var themeModel = ThemesModel.GetThemeModel(ThemeBases[CurrentTheme]);
            return themeModel.BaseColor;
        }
        return CurrentThemeDefaultBaseColor;
    }
    static ThemeModel CurrentThemeModel {
        get { return ThemesModel.Current.Groups.SelectMany(g => g.Themes).FirstOrDefault(t => t.Name == CurrentTheme); }
    }
    public static string[] CustomFonts {
        get {
            return new string[] {
                    CurrentThemeDefaultFont,
                    CurrentThemeDefaultFontSize + " " + "'Asap', normal",
                    CurrentThemeDefaultFontSize + " " + "'Arima Madurai', normal",
                    CurrentThemeDefaultFontSize + " " + "'Comfortaa', normal"
                };
        }
    }
    public static string CurrentThemeDefaultFont { get { return CurrentThemeModel.Font; } }
    public static string CurrentThemeDefaultFontSize { get { return CurrentThemeModel.FontSize; } }
    public static string CurrentThemeDefaultBaseColor { get { return CurrentThemeModel.BaseColor; } }
    public static string CurrentFont {
        get {
            if(Request.Cookies[CurrentFontCookieKey] != null)
                return HttpUtility.UrlDecode(Request.Cookies[CurrentFontCookieKey].Value);
            return CurrentThemeDefaultFont;
        }
    }

    static bool IsThemeChanged {
        get {
            return CurrentTheme != TunableTheme;
        }
    }
    static string TunableTheme {
        get {
            if(Request.Cookies[TunableThemeCookieKey] != null)
                return HttpUtility.UrlDecode(Request.Cookies[TunableThemeCookieKey].Value);
            return CurrentTheme;
        }
        set {
            Context.Response.Cookies[TunableThemeCookieKey].Value = value;
        }
    }
    static void SetCurrentBaseColorCookie(string value) {
        if(string.IsNullOrWhiteSpace(value))
            Request.Cookies.Remove(CurrentBaseColorCookieKey);
        else
            Request.Cookies[CurrentBaseColorCookieKey].Value = value;
        Context.Response.Cookies[CurrentBaseColorCookieKey].Value = value;
    }
    static void SetCurrentFontCookie(string value) {
        Context.Response.Cookies[CurrentFontCookieKey].Value = value;
    }

    public static bool CanApplyThemeParameters {
        get {
            return (!string.IsNullOrEmpty(CurrentThemeDefaultFont) || !string.IsNullOrEmpty(CurrentThemeDefaultBaseColor));
        }
    }
    public static void ResolveThemeParametes() {
        if(!CanChangeTheme)
            return;
        string baseColor = CurrentBaseColor;
        string font = CurrentFont;
        if(IsThemeChanged || !string.IsNullOrEmpty(baseColor) && !CustomBaseColors.Contains(baseColor) || baseColor == CurrentThemeDefaultBaseColor) {
            baseColor = string.Empty;
            SetCurrentBaseColorCookie(baseColor);
        }
        if(IsThemeChanged || !string.IsNullOrEmpty(font) && !CustomFonts.Contains(font) || font == CurrentThemeDefaultFont) {
            font = string.Empty;
            SetCurrentFontCookie(font);
        }
        TunableTheme = CurrentTheme;
        ASPxWebControl.GlobalThemeBaseColor = baseColor;
        ASPxWebControl.GlobalThemeFont = font;
    }
    static string GetShortFontName(string fullName) {
        if(string.IsNullOrWhiteSpace(fullName))
            return fullName;
        int indexOfFirstSpace = fullName.IndexOf(' ');
        int indexOfFirstComma = fullName.IndexOf(',');
        int endPosition = indexOfFirstComma > -1 ? indexOfFirstComma - 1 : fullName.Length - 1;
        return fullName.Substring(indexOfFirstSpace + 1, endPosition - indexOfFirstSpace).Trim('\'');
    }
    public static bool CanChangeBaseColor {
        get {
            return ThemesModel.Current.Groups.Where(g => g.Themes.Where(t => t.Name == CurrentTheme && !String.IsNullOrEmpty(t.BaseColor)).Count() != 0).Count() > 0;
        }
    }
    public static string GetColoredSquareUrl(string color) {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg'><g><rect height='100%' width='100%' y='0' x='0' fill='" +
            color + "'/></g></svg>";
        var svgEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg));
        return "data:image/svg+xml;base64," + svgEncoded;
    }

    public static string CurrentBaseColor {
        get {
            if(Request.Cookies[CurrentBaseColorCookieKey] != null)
                return HttpUtility.UrlDecode(Request.Cookies[CurrentBaseColorCookieKey].Value);
            return CurrentThemeDefaultBaseColor;
        }
    }
    
}

