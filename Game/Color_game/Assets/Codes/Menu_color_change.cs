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
    private float timer;

    private void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        random1 += (float)(r.Next(1, 100) / 1000f);
        random2 -= (float)(r.Next(1, 100) / 1000f);
        random3 += (float)(r.Next(1, 100) / 1000f);

        if (random1 >= 255) random1 = 0;
        if (random2 <= 0) random2 = 255;
        if (random3 >= 255) random3 = 0;

        if (timer > 1)
        {
            timer = 0;

            foreach (var item in texts)
            {
                item.GetComponent<TextMeshProUGUI>().color = new Color(random1, random2, random3);
            }
        }
    }
}
