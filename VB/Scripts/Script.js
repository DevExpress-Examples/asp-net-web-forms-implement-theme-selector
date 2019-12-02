(function () {
    var DXDemo = {};

    DXDemo.toggleNavigationPanel = function () {
        if (!window.NavigationPanel)
            return;

        if (window.innerWidth <= NavigationPanel.cpCollapseAtWindowInnerWidth) {
            if (!NavigationPanel.GetVisible())
                NavigationPanel.SetVisible(true);
            NavigationPanel.Toggle();
        } else {
            NavigationPanel.SetVisible(!NavigationPanel.GetVisible());
            ASPx.GetControlCollection().ProcessControlsInContainer(document.getElementById('DemoArea'), function (control) {
                control.AdjustControl();
            });
        }
        DXDemo.adjustDemoSettingsBlock();
    };
    DXDemo.toggleThemeSettingsPanel = function () {
        if (!window.ThemeSettingsPanel)
            return;
        ThemeSettingsPanel.Toggle();
    };

    DXDemo.CurrentThemeCookieKey = "DXCurrentTheme";
    DXDemo.CurrentBaseColorCookieKey = "DXCurrentBaseColor";
    DXDemo.CurrentAdaptiveThemeCookieKey = "DXCurrentAdaptiveTheme";
    DXDemo.CurrentAdaptiveBaseColorCookieKey = "DXCurrentAdaptiveBaseColor";
    DXDemo.CurrentFontCookieKey = "DXCurrentFont";
 
    DXDemo.SetCurrentAdaptiveTheme = function (theme) {
        ASPxClientUtils.SetCookie(DXDemo.CurrentAdaptiveThemeCookieKey, theme);
        forceReloadPage();
    }
    DXDemo.SetCurrentTheme = function (theme) {
        ASPxClientUtils.SetCookie(DXDemo.CurrentThemeCookieKey, theme);
        forceReloadPage();
    }
    DXDemo.OnShowAllThemesClick = function () {
        var themesListsNames = ShowAllThemesButton.cpThemesListsNames;
        for (var i = 0; i < themesListsNames.length; i++) {
            var themesList = window[themesListsNames[i]];
            themesList.SetVisible(!themesList.GetVisible());
        }
        var showAllThemesText = 'Show All Themes';
        var showTopThemesText = 'Show Top Themes';
        ShowAllThemesButton.SetText(ShowAllThemesButton.GetText() != showAllThemesText ? showAllThemesText : showTopThemesText);
    }
    DXDemo.OnThemesListSelectedIndexChanged = function (sender) {
        var selectedThemeName = sender.GetSelectedItem().value;
        if (ShowAllThemesButton.cpCurrenTheme != selectedThemeName)
            DXDemo.SetCurrentTheme(selectedThemeName !== 'Default' ? selectedThemeName : '');
    }
    DXDemo.OnThemesListInit = function (sender) {
        var mainElement = sender.GetMainElement();
        var itemElements = Array.prototype.slice.call(mainElement.getElementsByClassName("item"), 0);
        var newItemElements = itemElements.filter(function (elem) {
            return sender.cpNewThemes.indexOf(elem.textContent.trim()) !== -1;
        });
        newItemElements.forEach(function (itemElement) {
            if (itemElement.className.indexOf("new-item") === -1)
                itemElement.className += " new-item";
        });
    },
    DXDemo.SetCurrentFont = function (font) {
        ASPxClientUtils.SetCookie(DXDemo.CurrentFontCookieKey, font);
        forceReloadPage();
    }
    DXDemo.SetCurrentAdaptiveBaseColor = function (color) {
        ASPxClientUtils.SetCookie(DXDemo.CurrentAdaptiveBaseColorCookieKey, color);
        forceReloadPage();
    }
    DXDemo.SetCurrentBaseColor = function (color) {
        ASPxClientUtils.SetCookie(DXDemo.CurrentBaseColorCookieKey, color);
        forceReloadPage();
    }
    forceReloadPage = function () {
        if (document.forms[0] && (!document.forms[0].onsubmit
            || (document.forms[0].onsubmit.toString().indexOf("Sys.Mvc.AsyncForm") == -1 && !document.forms[0].hasAttribute("data-ajax")))) {
            // for export purposes
            var eventTarget = document.getElementById("__EVENTTARGET");
            if (eventTarget)
                eventTarget.value = "";
            var eventArgument = document.getElementById("__EVENTARGUMENT");
            if (eventArgument)
                eventArgument.value = "";

            document.forms[0].submit();
        } else {
            window.location.reload();
        }
    }
 
   
    DXDemo.iconButtonClick = function (s, e) {
        if (e.buttonIndex === 0)
            s.Focus();
    }

 
    function selectorPopupContainerElement_Click(popupControl, event) {
        var eventSource = ASPx.Evt.GetEventSource(event);
        if (eventSource && eventSource.className.indexOf('dxpc-content') >= 0) {
            popupControl.HideWindow();
        }
    }
    function DXSelectorPopupContainer_Init(sender) {
        var content = sender.GetWindowContentElement(-1);
        ASPxClientUtils.AttachEventToElement(content, 'click', function (event) { selectorPopupContainerElement_Click(sender, event); });
    }

    function DXThemeSettingsPopupContainer_Init(sender) {
        var content = sender.GetWindowContentElement(-1);
        var themesButtonWrapper = content.querySelector(".themes-button-wrapper");
        // ASPxClientUtils.AttachEventToElement(themesButtonWrapper, 'click', function(event) { ThemeParametersPopup.Hide(); ThemeSelectorPopup.Show(); });
    }

    window.DXDemo = DXDemo;
    window.DXSelectorPopupContainer_Init = DXSelectorPopupContainer_Init;
    window.DXThemeSettingsPopupContainer_Init = DXThemeSettingsPopupContainer_Init;
})();
