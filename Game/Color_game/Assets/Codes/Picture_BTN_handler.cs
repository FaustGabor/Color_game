using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Networking.UnityWebRequest;

public class Picture_BTN_handler : MonoBehaviour
{
    [SerializeField] private GameObject generated_pictures;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject game_ui;
    [SerializeField] private GameObject tile_selector_ui;

    // needed for colour picking
    [SerializeField] private GameObject picture_handler;
    [SerializeField] private GameObject selected_colour_ui;
    [SerializeField] private GameObject tile_ui;
    [SerializeField] private RectTransform picture_ui;

    private int p_height, p_width, screen_height, screen_width;
    private Texture2D picture;
    private Texture2D tile_picture;
    private Color[] tile_coloures = { Color.white, Color.white, Color.white, Color.white, Color.white };

    private void Start()
    {
        picture = picture_handler.GetComponent<Picture_handler>().selected_picture;
        p_height = picture.height;
        p_width = picture.width;
        screen_height = Screen.height;
        screen_width = Screen.width;

        tile_picture = new Texture2D(600, 600);
        Generate_tile(0);
    }

    public void Activate_generated_pictures()
    {
        generated_pictures.SetActive(true);
        menu.SetActive(false);
        game_ui.SetActive(false);
        tile_selector_ui.SetActive(false);
    }

    public void Activate_Menu()
    {
        generated_pictures.SetActive(false);
        menu.SetActive(true);
        game_ui.SetActive(false);
        tile_selector_ui.SetActive(false);
    }

    public void Activate_game()
    {
        generated_pictures.SetActive(false);
        menu.SetActive(false);
        game_ui.SetActive(true);
        tile_selector_ui.SetActive(false);
    }

    public void Load_Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Click_picture(BaseEventData data)
    {
        Vector2 result;
        PointerEventData point_data = data as PointerEventData;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(picture_ui, point_data.position, null, out result);

        result = new Vector2((result.x + 350), ((result.y + 350)));

        float x = (result.x * p_width) / 700;
        float y = (result.y * p_height) / 700;

        selected_colour_ui.GetComponent<RawImage>().color = picture.GetPixel((int)x, (int)y);
    }

    public void Activate_selector()
    {
        generated_pictures.SetActive(false);
        menu.SetActive(false);
        game_ui.SetActive(false);
        tile_selector_ui.SetActive(true);
    }

    public void Select_first()
    {
        tile_coloures[0] = selected_colour_ui.GetComponent<RawImage>().color;
        Generate_tile(0);
        Activate_game();
    }
    public void Select_second()
    {
        tile_coloures[1] = selected_colour_ui.GetComponent<RawImage>().color;
        Generate_tile(1);
        Activate_game();
    }
    public void Select_third()
    {
        tile_coloures[2] = selected_colour_ui.GetComponent<RawImage>().color;
        Generate_tile(2);
        Activate_game();
    }
    public void Select_fourth()
    {
        tile_coloures[3] = selected_colour_ui.GetComponent<RawImage>().color;
        Generate_tile(3);
        Activate_game();
    }
    public void Select_fifth()
    {
        tile_coloures[4] = selected_colour_ui.GetComponent<RawImage>().color;
        Generate_tile(4);
        Activate_game();
    }

    private void Generate_tile(int modified)
    {
        int k = 300;

        if (modified == 0)
        {
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 600; j++)
                {
                    tile_picture.SetPixel(i, j, tile_coloures[0]);
                    //tile_picture.SetPixel(i, j, Color.black);
                }
            }
        }

        // first diamond looking part 
        if (modified <= 1)
        {
            for (int i = 0; i < 300; i++)
            {
                for (int j = k; j <= (300 + i); j++)
                {
                    tile_picture.SetPixel(i, j, tile_coloures[1]);
                    //tile_picture.SetPixel(i, j, Color.black);
                }
                k--;
            }

            k = 300;
            for (int i = 599; i >= 300; i--)
            {
                for (int j = k; j <= (300 + (599 - i)); j++)
                {
                    tile_picture.SetPixel(i, j, tile_coloures[1]);
                    //tile_picture.SetPixel(i, j, Color.black);
                }
                k--;
            }
        }
        // first diamond looking part end

        if (modified <= 2)
        {
            for (int i = 150; i < 450; i++)
            {
                for (int j = 150; j < 450; j++)
                {
                    tile_picture.SetPixel(i, j, tile_coloures[2]);
                    //tile_picture.SetPixel(i, j, Color.black);
                }
            }
        }


        // second diamond looking part 
        if (modified <= 3)
        {
            k = 300;
            for (int i = 150; i < 300; i++)
            {
                for (int j = k; j <= (300 + (i - 150)); j++)
                {
                    tile_picture.SetPixel(i, j, tile_coloures[3]);
                    //tile_picture.SetPixel(i, j, Color.black);
                }
                k--;
            }


            k = 300;
            for (int i = 450; i >= 300; i--)
            {
                for (int j = k; j <= (300 + (450 - i)); j++)
                {
                    tile_picture.SetPixel(i, j, tile_coloures[3]);
                    //tile_picture.SetPixel(i, j, Color.black);
                }
                k--;
            }
        }
        // second diamond looking part end

        if (modified <= 4)
        {
            for (int i = 225; i < 375; i++)
            {
                for (int j = 225; j < 375; j++)
                {
                    tile_picture.SetPixel(i, j, tile_coloures[4]);
                    //tile_picture.SetPixel(i, j, Color.black);
                }
            }
        }

        tile_picture.Apply();
        tile_ui.GetComponent<RawImage>().texture = tile_picture;
    }
}
