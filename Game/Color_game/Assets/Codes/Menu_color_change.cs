using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Drawing;

public class Menu_color_change : MonoBehaviour
{
    public List<GameObject> texts;

    private System.Random r = new System.Random();
    private float timer;
    private int color_blocker;
    private float random1, random2, random3;
    private int sign1, sign2, sign3;

    private void Start()
    {
        timer = 0;

        random1 = r.Next(1, 255) / 255f;
        random2 = r.Next(1, 255) / 255f;
        random3 = r.Next(1, 255) / 255f;

        sign1 = 1;
        sign2 = 1;
        sign3 = 1;

        color_blocker = r.Next(1, 7);
    }

    void Update()
    {
        Select_Random_Color();

        /*
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            //timer = 0;
            //System.Drawing.Color randomColor = System.Drawing.Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));

            foreach (var item in texts)
            {
                //item.GetComponent<TextMeshProUGUI>().color = new UnityEngine.Color( randomColor.R/255f, randomColor.G/255f, randomColor.B/255f, 255f);
                item.GetComponent<TextMeshProUGUI>().color = new UnityEngine.Color(random1 / 255f, random2 / 255f, random3 / 255f, 255f);
            }
        }
        */
    }

    private void Select_Random_Color()
    {
        random1 += sign1 * (float)(r.Next(1, 100) / 1000f);
        random2 += sign2 * (float)(r.Next(1, 100) / 1000f);
        random3 += sign3 * (float)(r.Next(1, 100) / 1000f);

        if (random1 >= 255) sign1 = -1;
        if (random2 >= 255) sign2 = -1;
        if (random3 >= 255) sign3 = -1;

        if (random1 <= 0) sign1 = 1;
        if (random2 <= 0) sign2 = 1;
        if (random3 <= 0) sign3 = 1;

        switch (color_blocker)
        {
            case 1: random1 = 0; break;
            case 2: random2 = 0; break;
            case 3: random3 = 0; break;
            case 4: { random1 = 0; random2 = 0; break; }
            case 5: { random1 = 0; random3 = 0; break; }
            case 6: { random2 = 0; random3 = 0; break; }
            default: break;
        }

        foreach (var item in texts)
        {
            item.GetComponent<TextMeshProUGUI>().color = new UnityEngine.Color(random1 / 255f, random2 / 255f, random3 / 255f, 255f);
        }
    }
}
