using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture_handler : MonoBehaviour
{

    public Texture2D selected_picture;
    private Texture2D created_picture;
    public GameObject colour_ui;
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

        created_picture = new Texture2D(600, 600);

        Create_picture();

        colour_ui.GetComponent<RawImage>().texture = created_picture;
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

    private void Create_picture()
    {
        for (int i = 0; i < 600; i++)
        {
            for (int j = 0; j < 600; j++)
            {
                created_picture.SetPixel(i, j, colours[0]);
            }
        }

        // first diamond looking part 
        int k = 300;
        for (int i = 0; i < 300; i++)
        {
            for (int j = k; j <= (300 + i); j++)
            {
                created_picture.SetPixel(i, j, colours[1]);
            }
            k--;
        }

        k = 300;
        for (int i = 600; i >= 300; i--)
        {
            for (int j = k; j <= (300 + (600 - i)); j++)
            {
                created_picture.SetPixel(i, j, colours[1]);
            }
            k--;
        }
        // first diamond looking part end

        for (int i = 150; i < 450; i++)
        {
            for (int j = 150; j < 450; j++)
            {
                created_picture.SetPixel(i, j, colours[2]);
            }
        }

        // second diamond looking part 
        k = 300;
        for (int i = 150; i < 300; i++)
        {
            for (int j = k; j <= (300 + (i - 150)); j++)
            {
                created_picture.SetPixel(i, j, colours[3]);
            }
            k--;
        }

        
        k = 300;
        for (int i = 450; i >= 300; i--)
        {
            for (int j = k; j <= (300 + (450 - i)); j++)
            {
                created_picture.SetPixel(i, j, colours[3]);
            }
            k--;
        }
        // second diamond looking part end

        for (int i = 225; i < 375; i++)
        {
            for (int j = 225; j < 375; j++)
            {
                created_picture.SetPixel(i, j, colours[4]);
            }
        }






        /*
        for (int i = 300; i < 600; i++)
        {
            for (int j = 225; j < 375; j++)
            {
                created_picture.SetPixel(i, j, colours[4]);
            }
        }

        for (int i = 150; i < 300; i++)
        {
            for (int j = 225; j < 375; j++)
            {
                created_picture.SetPixel(i, j, colours[4]);
            }
        }

        for (int i = 300; i < 450; i++)
        {
            for (int j = 225; j < 375; j++)
            {
                created_picture.SetPixel(i, j, colours[4]);
            }
        }
        */

        created_picture.Apply();
    }
}
