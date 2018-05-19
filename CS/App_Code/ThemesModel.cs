using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;


[XmlRoot("Themes")]
public class ThemesModel {
    const string ResourceName = "DevExpress.Web.Demos.Data.Themes.xml";
    static ThemesModel _current;
    static readonly object _currentLock = new object();
    public static ThemesModel Current {
        get {
            lock(_currentLock) {
                if(_current == null) {
                    using(Stream stream = File.OpenRead(HttpContext.Current.Server.MapPath("~/App_Data/Themes.xml"))) {
                        XmlSerializer serializer = new XmlSerializer(typeof(ThemesModel));
                        _current = (ThemesModel)serializer.Deserialize(stream);
                    }
                }
                return _current;
            }
        }
    }
    List<ThemeGroupModel> _groups = new List<ThemeGroupModel>();
    public static IEnumerable<string> MobileThemes { get { return Current.Groups.First(g => g.Name == "Mobile").Themes.Select(t => t.Name); } }
    public static IEnumerable<ThemeModel> AllThemes { get { return Current.Groups.SelectMany(g => g.Themes); } }
    public static IEnumerable<ThemeModel> NewThemes { get { return AllThemes.Where(theme => theme.IsNew); } }
    public List<ThemeGroupModel> GetAllGroups() {
        var topThemes = AllThemes.Where(t => t.ShowAsTop).ToList();
        var currentTheme = AllThemes.First(t => t.Name == Utils.CurrentTheme);
        if(!topThemes.Contains(currentTheme))
            topThemes.Add(currentTheme);
        var firstGroup = new ThemeGroupModel() {
            Name = "FirstGroup",
            Themes = topThemes
        };
        return Groups.Concat(new[] { firstGroup }).ToList();
    }
    [XmlElement("ThemeGroup")]
    public List<ThemeGroupModel> Groups {
        get { return _groups; }
    }
    public List<ThemeGroupModel> LeftGroups {
        get { return (from g in Groups where g.Float == "Left" select g).ToList(); }
    }
    public List<ThemeGroupModel> RightGroups {
        get { return (from g in Groups where g.Float == "Right" select g).ToList(); }
    }
    public static ThemeModel GetThemeModel(string themeName) {
        return Current.Groups.SelectMany(g => g.Themes).FirstOrDefault(t => t.Name == themeName);
    }
}



