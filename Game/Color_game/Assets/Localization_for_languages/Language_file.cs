using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;
using UnityEngine.Localization;

public class Language_file : MonoBehaviour
{
    public string GetScores()
    {
        string point_ms = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "PointMs");
        string text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "DiamondGame") + ":\n" + PlayerPrefs.GetFloat("Diamond", 0).ToString()+" "+point_ms.Split(',')[0] + "\n\n"
                         + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "GreySlaceGame")+":\n"+ LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "AllColours")+": " + PlayerPrefs.GetFloat("GrayScale", 0).ToString() + point_ms.Split(',')[1]
                         + "\n" + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Vivid") +": "+ PlayerPrefs.GetFloat("GrayScaleVivid", 0).ToString() + point_ms.Split(',')[1]
                         + "\n" + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Pale") + ": " + PlayerPrefs.GetFloat("GrayScalePale", 0).ToString() + point_ms.Split(',')[1]
                         + "\n" + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Muted") + ": " + PlayerPrefs.GetFloat("GrayScaleMuted", 0).ToString() + point_ms.Split(',')[1]
                         + "\n" + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Dark") + ": " + PlayerPrefs.GetFloat("GrayScaleDark", 0).ToString() + point_ms.Split(',')[1];
        return text;
    }

    public void SetEN()
    {

        Locale lang = LocalizationSettings.AvailableLocales.Locales[0];
        LocalizationSettings.SelectedLocale = lang;
    }

    public void SetHU()
    {
        Locale lang = LocalizationSettings.AvailableLocales.Locales[1];
        LocalizationSettings.SelectedLocale = lang;
    }
}
