using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_handler : MonoBehaviour
{
    [SerializeField] List<GameObject> Vivid_colors;
    [SerializeField] List<GameObject> Pale_colors;
    [SerializeField] List<GameObject> Muted_colors;
    [SerializeField] List<GameObject> Dark_colors;

    [SerializeField] List<GameObject> spawned_obj;
    public List<GameObject> all_cubes;


    public GameObject timer;
    public GameObject btn_handler;

    public bool Check_right_positions()
    {
        if (spawned_obj.Count > 0)
        {
            foreach (var item in spawned_obj)
            {
                if (!item.GetComponent<Color_cube>().at_right_position)
                {
                    return false;
                }
            }
            Completed();
        }
        return true;
    }

    private void Completed() 
    {
        if (!SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            timer.GetComponent<Score_timer>().Save_Time();
        }
        btn_handler.GetComponent<Game_BTN_handler>().Load_Win();
    }
}
