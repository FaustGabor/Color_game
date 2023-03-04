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
        string text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "DiamondGame") + ":\n" + PlayerPrefs.GetFloat("Diamond", 0).ToString() + " " + point_ms.Split(',')[0] + "\n\n"
                         + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "GreySlaceGame") + ":\n" + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "AllColours") + ": " + PlayerPrefs.GetFloat("GrayScale", 0).ToString() + point_ms.Split(',')[1]
                         + "\n" + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "one_charactzer") + ": " + PlayerPrefs.GetFloat("GrayScaleType", 0).ToString() + point_ms.Split(',')[1]
                         + "\n" + LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "One_colour") + ": " + PlayerPrefs.GetFloat("GrayScaleOne", 0).ToString() + point_ms.Split(',')[1];
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

    public string Get_tutorial_text(int state)
    {
        string text = "";
        switch (state)
        {
            case 0: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Colour_tutorial_2"); break;
            case 1: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Colour_tutorial_3"); break;
            case 2: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Colour_tutorial_4"); break;
            case 3: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Colour_tutorial_5"); break;
            case 4: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Colour_tutorial_6"); break;
            case 5: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Colour_tutorial_7"); break;
            case 6: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Vivid"); break;
            case 7: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Pale"); break;
            case 8: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Muted"); break;
            case 9: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Dark"); break;
            default: text = LocalizationSettings.StringDatabase.GetLocalizedString("Texts", "Colour_tutorial_8"); break;
        }
        return text;
    }
}
