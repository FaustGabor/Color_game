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

    // needed for colour picking
    [SerializeField] private GameObject picture_handler;
    [SerializeField] private GameObject selected_colour_ui;
    [SerializeField] private RectTransform picture_ui;

    private int p_height, p_width, screen_height, screen_width;
    Texture2D picture;

    private void Start()
    {
        picture = picture_handler.GetComponent<Picture_handler>().selected_picture;
        p_height = picture.height;
        p_width = picture.width;
        screen_height = Screen.height;
        screen_width = Screen.width;
    }

    public void Activate_generated_pictures()
    {
        generated_pictures.SetActive(true);
        menu.SetActive(false);
        game_ui.SetActive(false);
    }

    public void Activate_Menu()
    {
        generated_pictures.SetActive(false);
        menu.SetActive(true);
        game_ui.SetActive(false);
    }

    public void Activate_game()
    {
        generated_pictures.SetActive(false);
        menu.SetActive(false);
        game_ui.SetActive(true);
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
}
