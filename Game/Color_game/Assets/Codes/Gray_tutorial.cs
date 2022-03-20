using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gray_tutorial : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject color;

    private float timer;
    private int state;

    private void Start()
    {
        timer = 0;
        state = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5)
        {
            switch (state)
            {
                case 0:
                    {
                        text.GetComponent<TextMeshProUGUI>().text = "Like so";
                        //color.GetComponent<RectTransform>().rect.x = -74f;
                        break;
                    }
                default: break;
            }
        }
    }
}
