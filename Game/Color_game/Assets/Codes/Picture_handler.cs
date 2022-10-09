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
    public GameObject colour_ui2;
    public GameObject colour_ui3;
    public GameObject picture_ui;

    public List<Color> colours = new List<Color>();
    public List<int> colour_number = new List<int>();

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
                    if (Colour_Distance(colours[k], c) < 0.03f) { found_similar = true; similar_index = k; break; }
                }
                if (!found_similar)
                {
                    colours.Add(c);
                    colour_number.Add(1);
                }
                else
                {
                    colour_number[similar_index]++;
                }
            }
        }

        /*
        Color most_dominant_c;
        int max = 0;
        for (int i = 0; i < colour_number.Count; i++)
        {
            if (colour_number[i] > max)
            {
                max = colour_number[i];
                most_dominant_c = colours[i];
            }
        }
        */

        Organize_colours();

        colour_ui.GetComponent<RawImage>().color = colours[0];
        colour_ui2.GetComponent<RawImage>().color = colours[1];
        colour_ui3.GetComponent<RawImage>().color = colours[2];
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

    private void Organize_colours()
    {
        for (int i = 0; i < colour_number.Count-1; i++)
        {
            int max = 0, index = 0;

            for (int j = i; j < colour_number.Count; j++)
            {
                if (colour_number[j] > max)
                {
                    max = colour_number[j];
                    index = j;
                }
            }

            Color temp_c = colours[i];
            colours[i] = colours[index];
            colours[index] = temp_c;

            int temp_n = colour_number[i];
            colour_number[i] = colour_number[index];
            colour_number[index] = temp_n;
        }
    }

}
