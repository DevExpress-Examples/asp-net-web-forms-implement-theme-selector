using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;



public class ThemeModel : ThemeModelBase {
    string _baseColor;
    string _fontFamily;
    string _fontSize;
    bool _showAsTop;
    string _previewColor;
    [XmlAttribute]
    public string BaseColor {
        get {
            if(String.IsNullOrEmpty(_baseColor))
                return "";
            return _baseColor;
        }
        set {
            _baseColor = value.ToUpper();
        }
    }
    [XmlAttribute]
    public bool IsNew { get; set; }
    [XmlAttribute]
    public string FontFamily {
        get {
            if(String.IsNullOrEmpty(_fontFamily))
                return "";
            return _fontFamily;
        }
        set {
            _fontFamily = value;
        }
    }
    [XmlAttribute]
    public string FontSize {
        get {
            if(String.IsNullOrEmpty(_fontSize))
                return "";
            return _fontSize;
        }
        set {
            _fontSize = value;
        }
    }
    public string Font {
        get {
            var result = string.Empty;
            if(!string.IsNullOrEmpty(FontSize) && !string.IsNullOrEmpty(FontFamily))
                result = string.Format("{0} {1}", FontSize, FontFamily);
            return result;
        }
    }
    [XmlAttribute]
    public bool ShowAsTop {
        get {
            return _showAsTop;
        }
        set {
            _showAsTop = value;
        }
    }
    [XmlAttribute]
    public string PreveiwColor {
        get {
            if(String.IsNullOrEmpty(_previewColor))
                return "";
            return _previewColor;
        }
        set {
            _previewColor = value.ToUpper();
        }
    }
}


