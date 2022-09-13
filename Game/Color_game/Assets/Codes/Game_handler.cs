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

    [SerializeField] List<GameObject> coloured_cubes_for_diamond; // diamond
    [SerializeField] List<GameObject> ghost_cubes_for_diamond; // diamond
    [SerializeField] int play_dimension; // diamond

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

    public void Add_Cube(bool left, GameObject obj)  // grey scale
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
        return false;
    }

    public void Completed() // grey scale
    {
        if (!SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            timer.GetComponent<Score_timer>().Save_Time();
        }
        btn_handler.GetComponent<Game_BTN_handler>().Load_Win();
    }

    public void Check_right_colors() // grey scale + diamond
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
        if (SceneManager.GetActiveScene().name.Contains("Diamond_Game"))
        {
            //A rossz helyen lévõ kockák lsitája
            List<GameObject> badones = GameObject.Find("GameHandler").GetComponent<Move_objects>().badcubes;

            //A rossz helyen lévõ kockák visszarakása az eredeti helyükre
            foreach (var item in badones)
            {
                foreach (var item2 in GameObject.Find("GameHandler").GetComponent<Random_OBJ_Placemant>().orginalpositions)
                {
                    if(item.name == item2.Key)
                    {
                        item.transform.position = item2.Value;
                    }
                }
            }
            GameObject.Find("GameHandler").GetComponent<Move_objects>().badcubes.Clear();
        }
    }

    public void Select_cube_list(string list) // grey scale 
    {
        switch (list)
        {
            case "vivid": selected_cubes = new List<GameObject>(Vivid_colors); break;
            case "muted": selected_cubes = new List<GameObject>(Muted_colors); break;
            case "pale": selected_cubes = new List<GameObject>(Pale_colors); break;
            case "dark": selected_cubes = new List<GameObject>(Dark_colors); break;
            case "one":
                {
                    selected_cubes = new List<GameObject>();
                    int random_hue = new System.Random().Next(0,8);
                    selected_cubes.Add(all_cubes[random_hue]);
                    selected_cubes.Add(all_cubes[random_hue + 9]);
                    selected_cubes.Add(all_cubes[random_hue + 18]);
                    selected_cubes.Add(all_cubes[random_hue + 27]);
                    break;
                }
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

    public void RandomDiamondColours()
    {
        all_cubes.Clear();
        ghost_cubes.Clear();

        int colour_package_number = coloured_cubes_for_diamond.Count / play_dimension;

        int r = new System.Random().Next(0, colour_package_number);

        for (int i = (r*play_dimension); i < ((r * play_dimension) + play_dimension); i++)
        {
            all_cubes.Add(coloured_cubes_for_diamond[i]);
            ghost_cubes.Add(ghost_cubes_for_diamond[i]);

            coloured_cubes_for_diamond[i].SetActive(true);
            ghost_cubes_for_diamond[i].SetActive(true);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Diamond_Game") && play_dimension != 0)
        { RandomDiamondColours(); this.gameObject.GetComponent<Random_OBJ_Placemant>().enabled = true; }
    }

}
