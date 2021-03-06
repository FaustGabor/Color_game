using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score_timer : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private GameObject text;

    void Start()
    {
    }

    void Update()
    {
        if(timer < (float.MaxValue-10))
        {
            timer += Time.deltaTime;
            text.GetComponent<TextMeshProUGUI>().text = timer.ToString("00.00")+" sec";
        }
    }

    public void Save_Time()
    {
        if (SceneManager.GetActiveScene().name.Contains("Gray"))
        {
            string selected_colours = GameObject.Find("Game_handler").GetComponent<Game_handler>().selected_colour;

            switch (selected_colours)
            { 
                case "vivid":
                    {
                        if (PlayerPrefs.GetFloat("GrayScaleVivid", 999) > timer)
                        { PlayerPrefs.SetFloat("GrayScaleVivid", timer); }
                        break;
                    } 
                case "muted":
                    {
                        if (PlayerPrefs.GetFloat("GrayScaleMuted", 999) > timer)
                        { PlayerPrefs.SetFloat("GrayScaleMuted", timer); }
                        break;
                    }
                case "pale":
                    {
                        if (PlayerPrefs.GetFloat("GrayScalePale", 999) > timer)
                        { PlayerPrefs.SetFloat("GrayScalePale", timer); }
                        break;
                    }
                case "dark":
                    {
                        if (PlayerPrefs.GetFloat("GrayScaleDark", 999) > timer)
                        { PlayerPrefs.SetFloat("GrayScaleDark", timer); }
                        break;
                    }
                default:
                    {
                        if (PlayerPrefs.GetFloat("GrayScale", 999) > timer)
                        { PlayerPrefs.SetFloat("GrayScale", timer); }
                        break;
                    }
            }
        }
        PlayerPrefs.Save();
    }
}
