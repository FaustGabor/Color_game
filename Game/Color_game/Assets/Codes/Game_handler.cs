using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class Game_handler : MonoBehaviour
{
    [SerializeField] List<GameObject> Vivid_colors; // grey scale
    [SerializeField] List<GameObject> Pale_colors; // grey scale
    [SerializeField] List<GameObject> Muted_colors; // grey scale
    [SerializeField] List<GameObject> Dark_colors; // grey scale

    [SerializeField] List<GameObject> spawned_obj_left; // grey scale
    [SerializeField] List<GameObject> spawned_obj_right; // grey scale

    public List<GameObject> all_cubes;  // grey scale + Diamond
    public List<GameObject> ghost_cubes; // Diamond
    private List<GameObject> selected_cubes; // grey scale

    public GameObject timer;
    public GameObject btn_handler;

    public GameObject green_check_picture;
    public string selected_colour;

    public GameObject Next_cube(bool left) // gery scale
    {
        if (selected_cubes == null) selected_cubes = all_cubes;

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

                    string[] partners = selected_cubes[index].GetComponent<Color_cube>().gray_partner.Split(','); // index-edik kocka szürke partnereinek lementése

                    foreach (var item in spawned_obj_left) // megnézi hogy a kiválasztott szürke partnerek helyén, van-e már másik kocka ha igen akkor másik kockát keres
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
                        foreach (var item in spawned_obj_right) // megnézi hogy a választott kocka nincs-e ott a másik oldalon már
                        {
                            if(item.name.Contains(selected_cubes[index].name))
                            { find = false; }
                        }
                    }
                    
                }
                while (index < selected_cubes.Count-1 && find == false);
            }

            if (find)
            {
                return selected_cubes[index];
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

                    string[] partners = selected_cubes[index].GetComponent<Color_cube>().gray_partner.Split(',');

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
                            if (item.name.Contains(selected_cubes[index].name))
                            { find = false; }
                        }
                    }
                }
                while (index < selected_cubes.Count - 1 && find == false);
            }

            if (find)
            {
                return selected_cubes[index];
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

    public bool Check_right_positions() // megnézi hogy minden kocka a jó helyen van-e
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

    public void Completed() 
    {
        if (!SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            timer.GetComponent<Score_timer>().Save_Time();
        }
        btn_handler.GetComponent<Game_BTN_handler>().Load_Win();
    }

    public void Check_right_colors()
    {
        if (SceneManager.GetActiveScene().name == "Gray_scale") // ha a kocka jó helyen van, akkor egy pipát rak melléjük
        {
            foreach (var item in spawned_obj_right)
            {
                if (item.GetComponent<Color_cube>().at_right_position)
                {
                    item.GetComponent<BoxCollider>().enabled = false;
                    GameObject obj = Instantiate(green_check_picture, item.transform.position + new Vector3(1f, 0, 0), green_check_picture.transform.rotation);
                    obj.transform.position += new Vector3(0, 0, -0.15f);
                }
            }

            foreach (var item in spawned_obj_left)
            {
                if (item.GetComponent<Color_cube>().at_right_position)
                {
                    item.GetComponent<BoxCollider>().enabled = false;
                    GameObject obj = Instantiate(green_check_picture, item.transform.position + new Vector3(-1f, 0, 0), green_check_picture.transform.rotation);
                    obj.transform.position += new Vector3(0, 0, -0.15f);
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Diamond_Game")
        {
            //A rossz helyen lévõ kockák lsitája
            List<GameObject> badones = GameObject.Find("EventHandler").GetComponent<Drag_And_Drop_3D>().badcubes;

            //A rossz helyen lévõ kockák visszarakása az eredeti helyükre
            foreach (var item in badones)
            {
                foreach (var item2 in GameObject.Find("EventHandler").GetComponent<Random_OBJ_Placemant>().orginalpositions)
                {
                    if(item.name == item2.Key)
                    {
                        item.transform.position = item2.Value;
                    }
                }
            }
            GameObject.Find("EventHandler").GetComponent<Drag_And_Drop_3D>().badcubes.Clear();
        }
    }

    public void Select_cube_list(string list)
    {
        switch (list)
        {
            case "vivid": selected_cubes = new List<GameObject>(Vivid_colors); break;
            case "muted": selected_cubes = new List<GameObject>(Muted_colors); break;
            case "pale": selected_cubes = new List<GameObject>(Pale_colors); break;
            case "dark": selected_cubes = new List<GameObject>(Dark_colors); break;
            default: selected_cubes = all_cubes; break;
        }
        selected_colour = list;

        System.Random r = new System.Random();
        int random = 0;

        for (int i = 0; i < selected_cubes.Count; i++) // Kocka lista randomizálása
        {
            random = r.Next(0, selected_cubes.Count);

            GameObject temp = selected_cubes[i];
            selected_cubes[i] = selected_cubes[random];
            selected_cubes[random] = temp;
        }
    }

}
