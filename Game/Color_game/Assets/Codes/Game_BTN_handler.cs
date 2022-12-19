using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_BTN_handler : MonoBehaviour
{
    public GameObject arrow; // grey scale tutorial
    public GameObject help; // grey scale tutorial
    public GameObject game_handler; // grey scale
    public GameObject menu_window; // grey scale
    public GameObject text; // diamond tutorial
    public GameObject vivid; // diamond tutorial
    public GameObject pale; // diamond tutorial
    public GameObject muted; // diamond tutorial
    public GameObject dark; // diamond tutorial
    public GameObject every_color; // diamond tutorial
    public GameObject finishdia; // diamondgame
    public GameObject game_type; // grey scale
    public GameObject spawners; // grey scale

    private int state = 0; // diamond tutorial

    public void Tutorial_next()
    {
        switch (state)
        {
            case 0:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    vivid.SetActive(true);
                    break;
                }
            case 1:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    vivid.SetActive(false);
                    pale.SetActive(true);
                    break;
                }
            case 2:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    vivid.SetActive(true);
                    vivid.transform.position += new Vector3(0, 0, 0.2f);
                    pale.SetActive(true);
                    break;
                }
            case 3:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    vivid.SetActive(false);
                    pale.SetActive(false);
                    muted.SetActive(true);
                    break;
                }
            case 4:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    dark.SetActive(true);
                    //muted.SetActive(false);
                    break;
                }
            case 5:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    muted.SetActive(true);
                    muted.transform.position += new Vector3(0, 0, 0.2f);
                    break;
                }

            case 6:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    every_color.SetActive(false);
                    vivid.SetActive(true);
                    pale.SetActive(false);
                    muted.SetActive(false);
                    dark.SetActive(false);

                    vivid.transform.position = Vector3.zero;
                    pale.transform.position = Vector3.zero;
                    muted.transform.position = Vector3.zero;
                    dark.transform.position = Vector3.zero;
                    break;
                }
            case 7:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    vivid.SetActive(false);
                    pale.SetActive(true);
                    muted.SetActive(false);
                    dark.SetActive(false);

                    vivid.transform.position = Vector3.zero;
                    pale.transform.position = Vector3.zero;
                    muted.transform.position = Vector3.zero;
                    dark.transform.position = Vector3.zero;
                    break;
                }
            case 8:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    vivid.SetActive(false);
                    pale.SetActive(false);
                    muted.SetActive(true);
                    dark.SetActive(false);

                    vivid.transform.position = Vector3.zero;
                    pale.transform.position = Vector3.zero;
                    muted.transform.position = Vector3.zero;
                    dark.transform.position = Vector3.zero;
                    break;
                }
            case 9:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    vivid.SetActive(false);
                    pale.SetActive(false);
                    muted.SetActive(false);
                    dark.SetActive(true);

                    vivid.transform.position = Vector3.zero;
                    pale.transform.position = Vector3.zero;
                    muted.transform.position = Vector3.zero;
                    dark.transform.position = Vector3.zero;
                    break;
                }
            default:
                {
                    text.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<Language_file>().Get_tutorial_text(state);
                    every_color.SetActive(true);
                    vivid.SetActive(false);
                    pale.SetActive(false);
                    muted.SetActive(false);
                    dark.SetActive(false);
                    state = 5;
                    break;
                }
        }
        state++;
    }

    public void Load_Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Load_Win()
    {
        if (SceneManager.GetActiveScene().name.Contains("Diamond_Game"))
        {
            if (GameObject.Find("GameHandler").GetComponent<Move_objects>().goodcubes.Count == 36)
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                Load_Menu();
            }
        }
        else
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void Help() // grey scale tutorial
    {
        help.SetActive(true);
        if ( (!SceneManager.GetActiveScene().name.Contains("Diamond_Game")) && (!SceneManager.GetActiveScene().name.Contains("Colour_orginazer")))
        {
            if (arrow != null) arrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(400f, -614f, 0f);
            help.transform.GetChild(1).gameObject.SetActive(true);
            help.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void End_Help() // grey scale tutorial
    {
        help.SetActive(false);

        if (help.transform.childCount >= 4) help.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void Finish() // grey scale finish game
    {
        help.transform.GetChild(1).gameObject.SetActive(false);
        help.transform.GetChild(2).gameObject.SetActive(true);
        if (arrow != null)
        {
            arrow.SetActive(false);
        }
        if (!game_handler.GetComponent<Game_handler>().Check_right_positions()) // Ha igaz, akkor a Check_right_positions függvény már átvisz minket a következõ scene-re
        {
            help.SetActive(true);
            if (menu_window != null) menu_window.SetActive(false);
            if (help.transform.childCount >= 4) help.transform.GetChild(3).gameObject.SetActive(false);

        }
    }

    public void Close_Help() // diamond
    {
        help.SetActive(false);
    }

    public void Diamond_Help_back()
    {
        if (state == 0) Close_Help();
        else
        {
            help.transform.GetChild(state + 1).gameObject.SetActive(true);
            help.transform.GetChild(state + 2).gameObject.SetActive(false);
            state--;
        }
    }

    public void Diamond_Help_forward()
    {
        if (state < 4)
        {
            state++;
            help.transform.GetChild(state + 1).gameObject.SetActive(false);
            help.transform.GetChild(state + 2).gameObject.SetActive(true);
        }
    }

    public void Finish_Dia()
    {
        finishdia.SetActive(true);
        menu_window.SetActive(false);

        //A jó helyen lévõ kockák megszámolása majd kiíratás mint pontszám
        finishdia.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Your score is: " + GameObject.Find("GameHandler").GetComponent<Move_objects>().goodcubes.Count+"/36";
        if (SceneManager.GetActiveScene().name.Contains("Diamond"))
        {
            PlayerPrefs.SetFloat("Diamond", GameObject.Find("GameHandler").GetComponent<Move_objects>().goodcubes.Count);
        }
        PlayerPrefs.Save();

    }

    public void Reveal_Menu()
    {
        menu_window.SetActive(true);
    }

    public void Hide_Menu()
    {
        menu_window.SetActive(false);
    }
    public void Check_Colors()
    {
        game_handler.GetComponent<Game_handler>().Check_right_colors();
        menu_window.SetActive(false);
    }

    public void Set_grayscale_to_character()
    {
        System.Random r = new System.Random();
        int random_num = r.Next(0, 40);
        switch (random_num/10)
        {
            case 0: game_handler.GetComponent<Game_handler>().Select_cube_list("vivid"); break;
            case 1: game_handler.GetComponent<Game_handler>().Select_cube_list("dark"); break;
            case 2: game_handler.GetComponent<Game_handler>().Select_cube_list("pale"); break;
            case 3: game_handler.GetComponent<Game_handler>().Select_cube_list("muted"); break;
            default: game_handler.GetComponent<Game_handler>().Select_cube_list("vivid"); break;
        }
        
        game_type.SetActive(false);
        spawners.SetActive(true);
    }
    public void Set_grayscale_to_all()
    {
        game_handler.GetComponent<Game_handler>().Select_cube_list("");
        game_type.SetActive(false);
        spawners.SetActive(true);
    }

    public void Set_grayscale_to_one()
    {
        game_handler.GetComponent<Game_handler>().Select_cube_list("one");
        game_type.SetActive(false);
        spawners.SetActive(true);
    }

    public void Move_to_wheel() // colour tutorial 2
    {
        game_handler.GetComponent<Colour_Wheel_Handler>().Move_cubes_to_circle();
    }

    public void Move_back_from_wheel() // colour tutorial 2
    {
        game_handler.GetComponent<Colour_Wheel_Handler>().Move_cubes_back();
    }
}
