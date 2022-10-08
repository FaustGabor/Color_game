using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class Picture_handler : MonoBehaviour
{
    [SerializeField] private Folder picture_folder;

    public Texture2D selected_picture;
    public GameObject colour_ui;
    public GameObject picture_ui;

    public List<Color> colours = new List<Color>();
    public List<int> colour_number = new List<int>();
    public Color most_dominant_c;

    public List<Color> colours_d20 = new List<Color>();
    public List<Color> colours_d50 = new List<Color>();
    public List<Color> colours_d10 = new List<Color>();

    private void Start()
    {
        int similar_index = 0;
        bool found_similar;
        for (int i = 0; i < selected_picture.width; i++)
        {
            for (int j = 0; j < selected_picture.height; j++)
            {
                Color c = selected_picture.GetPixel(i, j);
                found_similar = false;
                for (int k = 0; k < colours.Count; k++)
                {
                    if (Colour_Distance(colours[k], c) < 0.05f) { found_similar = true; similar_index = k; break; }
                }
                if (!found_similar)
                {
                    colours.Add(c);
                    colour_number.Add(0);
                }
                else
                {
                    colour_number[similar_index]++;
                }
            }
        }

        
        int max = 0;
        for (int i = 0; i < colour_number.Count; i++)
        {
            if (colour_number[i] > max)
            {
                max = colour_number[i];
                most_dominant_c = colours[i];
            }
        }

        colour_ui.GetComponent<RawImage>().color = most_dominant_c;
        picture_ui.GetComponent<RawImage>().texture = selected_picture;

    }

    private float Colour_Distance(Color first, Color second)
    {
        float r_dist = 0;
        float g_dist = 0;
        float b_dist = 0;

        r_dist = Mathf.Abs(first.r - second.r);
        g_dist = Mathf.Abs(first.g - second.g);
        b_dist = Mathf.Abs(first.b - second.b);

        return ((r_dist + g_dist + b_dist) / 3);
        
    }

}
