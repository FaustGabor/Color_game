using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Diamond_chooser : MonoBehaviour
{
    public void Game1()
    {
        SceneManager.LoadScene("Diamond_Game");
    }

    public void Game2()
    {
        SceneManager.LoadScene("Diamond_Game_2x2");
    }

    public void To_menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
