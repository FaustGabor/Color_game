using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu_BTN_Handler : MonoBehaviour
{
    [SerializeField] private GameObject game_selector;
    [SerializeField] private GameObject title_screen;
    [SerializeField] private GameObject start_menu;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject info;
    [SerializeField] private GameObject language;

    public void Change_To_Games()
    {
        game_selector.SetActive(true);
        start_menu.SetActive(false);
        tutorial.SetActive(false);
        score.SetActive(false);
        title_screen.SetActive(false);
    }

    public void Change_To_Tutorials()
    {
        game_selector.SetActive(false);
        start_menu.SetActive(false);
        tutorial.SetActive(true);
        score.SetActive(false);
        title_screen.SetActive(false);
    }

    public void Change_To_Scores()
    {
        game_selector.SetActive(false);
        start_menu.SetActive(false);
        tutorial.SetActive(false);
        score.SetActive(true);
        title_screen.SetActive(false);

        score.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = language.GetComponent<Language_file>().GetScores();
    }
    public void Change_To_Info()
    {
        info.SetActive(true);
        start_menu.SetActive(false);
        tutorial.SetActive(false);
        score.SetActive(false);
        game_selector.SetActive(false);
        title_screen.SetActive(false);
    }
    public void Change_To_Menu() 
    {
        start_menu.SetActive(true);
        info.SetActive(false);
        tutorial.SetActive(false);
        score.SetActive(false);
        game_selector.SetActive(false);
        title_screen.SetActive(false);
    }

    public void Change_To_Title()
    {
        start_menu.SetActive(false);
        info.SetActive(false);
        tutorial.SetActive(false);
        score.SetActive(false);
        game_selector.SetActive(false);
        title_screen.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Load_Color_Organizer()
    {
        SceneManager.LoadScene("Diamond_chooser");
    }

    public void Load_Gray_Scale()
    {
        SceneManager.LoadScene("Gray_Scale");
    }

    public void Load_Tutorial_Gray_Scale()
    {
        SceneManager.LoadScene("Gray_Tutorial");
    }

    public void Load_Tutorial_Colors()
    {
        SceneManager.LoadScene("Colour_tutorial");
    }

    public void Load_Tutorial_Colors2()
    {
        SceneManager.LoadScene("Colour_orginazer_Game");
    }

    public void Set_Language_EN()
    {
        language.GetComponent<Language_file>().SetEN();
        PlayerPrefs.SetString("lang", "EN");
        PlayerPrefs.Save();
    }

    public void Set_Language_HU()
    {
        language.GetComponent<Language_file>().SetHU();
        PlayerPrefs.SetString("lang", "HU");
        PlayerPrefs.Save();
    }

    
    private void Start()
    {
        if (PlayerPrefs.GetString("lang") == "EN")
        { Set_Language_EN(); }
        else
        { Set_Language_HU(); }

        if (PlayerPrefs.GetString("tutorial") == "Done")
        {
            start_menu.transform.GetChild(2).GetComponent<Button>().interactable = true;
        }
        else
        {
            start_menu.transform.GetChild(2).GetComponent<Button>().interactable = false;
        }
    }
}
