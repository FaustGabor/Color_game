using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_BTN_handler : MonoBehaviour
{
    public GameObject arrow;
    public GameObject help;
    public GameObject game_handler;

    public void Load_Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Load_Win()
    {
        SceneManager.LoadScene("Win");
    }

    public void Help()
    {
        help.SetActive(true);
        arrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(341f,-614f,0f);
    }

    public void End_Help()
    {
        help.SetActive(false);
        help.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "In this game, you have to match the colored objects wit their gray partner.\n\nClick on the purple square, and click next to the scale.\n\nMove the colored square up/down on the scale, and find the gray square that match the color.";
    }

    public void Finish()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);
        }
        if (!game_handler.GetComponent<Game_handler>().Check_right_positions())
        {
            help.SetActive(true);
            help.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You have not found the right grey partners yet.";
        }
    }
}
