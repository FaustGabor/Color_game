using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_BTN_Handler : MonoBehaviour
{
    public GameObject game_selector;
    public GameObject start_menu;
    public GameObject tutorial;

    public void Change_To_Games()
    {
        game_selector.SetActive(true);
        start_menu.SetActive(false);
        tutorial.SetActive(false);
    }

    public void Change_To_Tutorials()
    {
        game_selector.SetActive(false);
        start_menu.SetActive(false);
        tutorial.SetActive(true);
    }

    public void Load_Color_Organizer()
    {
        SceneManager.LoadScene("Color_Organizer");
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
        SceneManager.LoadScene("Color_Tutorial");
    }
    public void Load_Tutorial_Color_Organizer()
    {
        SceneManager.LoadScene("Color_Organizer_T");
    }
}
