using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu_BTN_Handler : MonoBehaviour
{
    public GameObject game_selector;
    public GameObject start_menu;
    public GameObject tutorial;
    public GameObject score;

    public void Change_To_Games()
    {
        game_selector.SetActive(true);
        start_menu.SetActive(false);
        tutorial.SetActive(false);
        score.SetActive(false);
    }

    public void Change_To_Tutorials()
    {
        game_selector.SetActive(false);
        start_menu.SetActive(false);
        tutorial.SetActive(true);
        score.SetActive(false);
    }

    public void Change_To_Scores()
    {
        game_selector.SetActive(false);
        start_menu.SetActive(false);
        tutorial.SetActive(false);
        score.SetActive(true);

        score.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Diamond game:\n" + PlayerPrefs.GetFloat("Diamond",999).ToString() + " mp\n\n Grey scale game:\n" + PlayerPrefs.GetFloat("GrayScale",999).ToString() + " mp";
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Load_Color_Organizer()
    {
        SceneManager.LoadScene("Diamond_Game");
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
