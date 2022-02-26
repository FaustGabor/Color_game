using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Menu_color_change : MonoBehaviour
{

    public List<GameObject> texts;
    public float random1, random2, random3;
    private System.Random r = new System.Random();

    void Update()
    {
        random1 += (float)(r.Next(1,100) / 1000);
        random2 += (float)(r.Next(1,100) / 1000);
        random3 += (float)(r.Next(1,100) / 1000);

        if (random1 >= 255) random1 = 0;
        if (random2 >= 255) random2 = 0;
        if (random3 >= 255) random3 = 0;

        foreach (var item in texts)
        {
            item.GetComponent<TextMeshProUGUI>().color = new Color(random1, random2, random3);
        }
    }
}
