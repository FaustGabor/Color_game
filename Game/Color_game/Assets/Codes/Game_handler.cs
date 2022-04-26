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
        int index = -1;
        if (left) index = 0;
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

                    string[] partners = all_cubes[index].GetComponent<Color_cube>().gray_partner.Split(',');

                    foreach (var item in spawned_obj_left)
                    {
                        foreach (var item2 in partners)
                        {
                            if (!item.GetComponent<Color_cube>().gray_partner.Contains(item2))
                            {
                                find = true;
                            }
                            else
                            {
                                find = false;
                                break;
                            }
                        }

                        if (find == false) break;
                    }

                    if (find)
                    {
                        foreach (var item in spawned_obj_right)
                        {
                            if(item.name.Contains(all_cubes[index].name))
                            { find = false; }
                        }
                    }
                    
                }
                while (index < 35 && find == false);
            }

            Debug.Log("left" + index);

            if (find)
            {
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

                    string[] partners = all_cubes[index].GetComponent<Color_cube>().gray_partner.Split(',');

                    foreach (var item in spawned_obj_right)
                    {
                        foreach (var item2 in partners)
                        {
                            if (!item.GetComponent<Color_cube>().gray_partner.Contains(item2))
                            {
                                find = true;
                            }
                            else
                            {
                                find = false;
                                break;
                            }
                        }

                        if (find == false) break;
                    } 

                    if (find)
                    {
                        foreach (var item in spawned_obj_left)
                        {
                            if (item.name.Contains(all_cubes[index].name))
                            { find = false; }
                        }
                    }
                }
                while (index < 35 && find == false);
            }

            Debug.Log("right" + index);

            if (find)
            {
                return all_cubes[index];
            }
        }

        return null;
    }

    public void Add_Cube(bool left, GameObject obj)
    {
        if (left)
        {
            spawned_obj_left.Add(obj);
        }
        else
        {
            spawned_obj_right.Add(obj);
        }
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
        System.Random r = new System.Random();
        int random = 0;

        for (int i = 0; i < all_cubes.Count; i++)
        {
            random = r.Next(0, all_cubes.Count);

            GameObject temp = all_cubes[i];
            all_cubes[i] = all_cubes[random];
            all_cubes[random] = temp;
        }
    }
}
