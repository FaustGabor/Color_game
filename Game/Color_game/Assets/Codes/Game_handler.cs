using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class Game_handler : MonoBehaviour
{
    [SerializeField] List<GameObject> Vivid_colors;
    [SerializeField] List<GameObject> Pale_colors;
    [SerializeField] List<GameObject> Muted_colors;
    [SerializeField] List<GameObject> Dark_colors;

    [SerializeField] List<GameObject> spawned_obj_left;
    [SerializeField] List<GameObject> spawned_obj_right;

    public List<GameObject> all_cubes;
    public List<GameObject> ghost_cubes;

    public GameObject timer;
    public GameObject btn_handler;

    public GameObject Next_cube(bool left)
    {
        int index = 0;
        bool find = true;

        if (left)
        {
            //if (spawned_obj_left.Count > 0)
            {
                //find = false;
                do
                {
                    find = true;
                    index++;
                    foreach (var item in spawned_obj_left)
                    {
                        if (item.GetComponent<Color_cube>().gray_partner.Length > all_cubes[index].GetComponent<Color_cube>().gray_partner.Length)
                        {
                            if (!item.GetComponent<Color_cube>().gray_partner.Contains(all_cubes[index].GetComponent<Color_cube>().gray_partner))
                            {
                                find = true;
                            }
                            else
                            {
                                find = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!all_cubes[index].GetComponent<Color_cube>().gray_partner.Contains(item.GetComponent<Color_cube>().gray_partner))
                            {
                                find = true;
                            }
                            else
                            {
                                find = false;
                                break;
                            }
                        }
                    }

                    if (find)
                    {
                        foreach (var item in spawned_obj_right)
                        {
                            if(item.name == all_cubes[index].name)
                            { find = false; }
                        }
                    }
                    
                }
                while (index < 35 && find == false);
            }

            Debug.Log("left" + index);

            if (find)
            {
                spawned_obj_left.Add(all_cubes[index]);
                return all_cubes[index];
            }
        }
        else
        {
            //if (spawned_obj_right.Count > 0)
            {
                //find = false;
                do
                {
                    find = true;
                    index++;
                    foreach (var item in spawned_obj_right)
                    {
                        if (item.GetComponent<Color_cube>().gray_partner.Length > all_cubes[index].GetComponent<Color_cube>().gray_partner.Length)
                        {
                            if (!item.GetComponent<Color_cube>().gray_partner.Contains(all_cubes[index].GetComponent<Color_cube>().gray_partner))
                            {
                                find = true;
                            }
                            else
                            {
                                find = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!all_cubes[index].GetComponent<Color_cube>().gray_partner.Contains(item.GetComponent<Color_cube>().gray_partner))
                            {
                                find = true;
                            }
                            else
                            {
                                find = false;
                                break;
                            }
                        }
                    }

                    if (find)
                    {
                        foreach (var item in spawned_obj_left)
                        {
                            if (item.name == all_cubes[index].name)
                            { find = false; }
                        }
                    }
                }
                while (index < 35 && find == false);
            }

            Debug.Log("right" + index);

            if (find)
            {
                spawned_obj_right.Add(all_cubes[index]);
                return all_cubes[index];
            }
        }

        return null;
    }


    public bool Check_right_positions()
    {
        if (spawned_obj_left.Count > 0 || spawned_obj_right.Count > 0)
        {
            foreach (var item in spawned_obj_left)
            {
                if (!item.GetComponent<Color_cube>().at_right_position)
                {
                    return false;
                }
            }

            foreach (var item in spawned_obj_right)
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

    private void Start()
    {
        all_cubes.OrderBy(x => Guid.NewGuid()).ToList();
    }
}
