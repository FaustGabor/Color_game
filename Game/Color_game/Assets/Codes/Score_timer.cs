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
            PlayerPrefs.SetFloat("GrayScale", timer);
        }
        if (SceneManager.GetActiveScene().name.Contains("Diamond"))
        {
            PlayerPrefs.SetFloat("Diamond", timer);
        }
        PlayerPrefs.Save();
    }
}
