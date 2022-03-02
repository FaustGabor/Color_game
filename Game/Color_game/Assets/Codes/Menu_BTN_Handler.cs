using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_BTN_Handler : MonoBehaviour
{
    public GameObject game_selector;
    public GameObject start_menu;

    public void Change_To_Games()
    {
        game_selector.SetActive(true);
        start_menu.SetActive(false);
    }

    public void Load_Color_Organizer()
    {
        SceneManager.LoadScene("Color_Organizer");
    }
}
