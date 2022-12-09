using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class Colour_Wheel_Handler : MonoBehaviour
{
    private int circle_index = 0;

    [SerializeField] private List<GameObject> circle0;
    [SerializeField] private List<GameObject> circle1;
    [SerializeField] private List<GameObject> circle2;
    [SerializeField] private List<GameObject> circle3;

    [SerializeField] private List<GameObject> cubes0;
    [SerializeField] private List<GameObject> cubes1;
    [SerializeField] private List<GameObject> cubes2;
    [SerializeField] private List<GameObject> cubes3;

    [SerializeField] private List<GameObject> circle_o;

    [SerializeField] private GameObject text;

    private bool problem_with_circle_so_wait = false;
    private float timer;
    private int time_to_wait;

    private List<GameObject> Short_Cubes(List<GameObject> cubes)
    {
        int min, index;

        for (int i = 0; i < cubes.Count; i++)
        {
            min = 10;
            index = 0;
            for (int j = i; j < cubes.Count; j++)
            {
                int number = int.Parse(cubes[j].GetComponent<Color_cube>().gray_partner);
                if (number < min) { min = number; index = j; }
            }
            GameObject temp = cubes[i];
            cubes[i] = cubes[index];
            cubes[index] = temp;
        }

        return cubes;
    }

    public void Move_cubes_to_circle()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Drag");
        List<GameObject> cloned_cubes = new List<GameObject>();
        bool all_one_chacacter = true;


        // check cubes in circle
        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i].name.Contains("Clone")) { cloned_cubes.Add(cubes[i]); }
        }

        // check if all of them are vivid/pale/muted/dark
        for (int i = 0; i < cloned_cubes.Count; i++)
        {
            switch (circle_index)
            {
                case 0:
                    if (!cloned_cubes[i].name.Contains("Vivid")) all_one_chacacter = false; break;  
                case 1:
                    if (!cloned_cubes[i].name.Contains("Pale")) all_one_chacacter = false; break;
                case 2:
                    if (!cloned_cubes[i].name.Contains("Muted")) all_one_chacacter = false; break;
                case 3:
                    if (!cloned_cubes[i].name.Contains("Dark")) all_one_chacacter = false; break;
                default: break;
            }
        }

        if (cloned_cubes.Count == 9)
        {
            if (all_one_chacacter)
            {
                cloned_cubes = Short_Cubes(cloned_cubes);

                // check order of colours
                bool order_is_right = true;

                int yellow_index = int.Parse(cloned_cubes.Find(x => x.name.Contains("Y")).GetComponent<Color_cube>().gray_partner);
                char[] colour_order = { 'O', 'R', 'P', 'V', 'B', 'T', 'G', 'A' };
                int[] number_order = { 6, 5, 4, 9, 1, 8, 7, 3, 2 };
                yellow_index = Array.IndexOf(number_order, yellow_index);

                for (int i = 1; i < 9; i++)
                {
                    if (!cloned_cubes[number_order[i]-1].name[cloned_cubes[number_order[i] - 1].name.Length - 8].Equals(colour_order[i-1]))
                    {
                        order_is_right = false;
                    }
                }

                if (order_is_right)
                {
                    switch (circle_index)
                    {
                        case 0:
                            {
                                for (int i = 0; i < cloned_cubes.Count; i++)
                                {
                                    cloned_cubes[i].transform.position = circle0[i].transform.position;
                                    cloned_cubes[i].transform.position += Vector3.up / 2;
                                    cloned_cubes[i].transform.rotation = circle0[i].transform.rotation;
                                    cloned_cubes[i].GetComponent<Color_cube>().color_name = cloned_cubes[i].name;
                                    cloned_cubes[i].name = "Cube" + circle0[i].name;
                                    cubes0 = cloned_cubes;
                                }
                                circle_index++;
                                break;
                            }
                        case 1:
                            {
                                for (int i = 0; i < cloned_cubes.Count; i++)
                                {
                                    cloned_cubes[i].transform.position = circle1[i].transform.position;
                                    cloned_cubes[i].transform.position += Vector3.up / 2;
                                    cloned_cubes[i].transform.rotation = circle1[i].transform.rotation;
                                    cloned_cubes[i].GetComponent<Color_cube>().color_name = cloned_cubes[i].name;
                                    cloned_cubes[i].name = "Cube" + circle1[i].name;
                                    cubes1 = cloned_cubes;
                                }
                                circle_index++;
                                break;
                            }
                        case 2:
                            {
                                for (int i = 0; i < cloned_cubes.Count; i++)
                                {
                                    cloned_cubes[i].transform.position = circle2[i].transform.position;
                                    cloned_cubes[i].transform.position += Vector3.up / 2;
                                    cloned_cubes[i].transform.rotation = circle2[i].transform.rotation;
                                    cloned_cubes[i].GetComponent<Color_cube>().color_name = cloned_cubes[i].name;
                                    cloned_cubes[i].name = "Cube" + circle2[i].name;
                                    cubes2 = cloned_cubes;
                                }
                                circle_index++;
                                break;
                            }
                        case 3:
                            {
                                for (int i = 0; i < cloned_cubes.Count; i++)
                                {
                                    cloned_cubes[i].transform.position = circle3[i].transform.position;
                                    cloned_cubes[i].transform.position += Vector3.up / 2;
                                    cloned_cubes[i].transform.rotation = circle3[i].transform.rotation;
                                    cloned_cubes[i].GetComponent<Color_cube>().color_name = cloned_cubes[i].name;
                                    cloned_cubes[i].name = "Cube" + circle3[i].name;
                                    cubes3 = cloned_cubes;
                                }
                                circle_index++;
                                break;
                            }
                        default: break;
                    }

                    Change_text_in_circle();
                }
                else
                {
                    text.transform.GetChild(0).gameObject.SetActive(false);
                    text.transform.GetChild(1).gameObject.SetActive(false);
                    text.transform.GetChild(2).gameObject.SetActive(false);
                    text.transform.GetChild(3).gameObject.SetActive(false);
                    text.transform.GetChild(4).gameObject.SetActive(false);
                    text.transform.GetChild(5).gameObject.SetActive(false);
                    text.transform.GetChild(6).gameObject.SetActive(true);
                    text.transform.GetChild(7).gameObject.SetActive(false);
                    problem_with_circle_so_wait = true;
                    time_to_wait = 6;
                }
            }
            else
            {
                text.transform.GetChild(0).gameObject.SetActive(false);
                text.transform.GetChild(1).gameObject.SetActive(false);
                text.transform.GetChild(2).gameObject.SetActive(false);
                text.transform.GetChild(3).gameObject.SetActive(false);
                text.transform.GetChild(4).gameObject.SetActive(true);
                text.transform.GetChild(5).gameObject.SetActive(false);
                text.transform.GetChild(6).gameObject.SetActive(false);
                text.transform.GetChild(7).gameObject.SetActive(false);
                problem_with_circle_so_wait = true;
                time_to_wait = 6;
            }
        }
        else
        {
            text.transform.GetChild(0).gameObject.SetActive(false);
            text.transform.GetChild(1).gameObject.SetActive(false);
            text.transform.GetChild(2).gameObject.SetActive(false);
            text.transform.GetChild(3).gameObject.SetActive(false);
            text.transform.GetChild(4).gameObject.SetActive(false);
            text.transform.GetChild(5).gameObject.SetActive(true);
            text.transform.GetChild(6).gameObject.SetActive(false);
            text.transform.GetChild(7).gameObject.SetActive(false);
            problem_with_circle_so_wait = true;
            time_to_wait = 3;
        }
    }

    public void Move_cubes_back()
    {

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Drag");
        List<GameObject> cloned_cubes = new List<GameObject>();

        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i].name.Contains("Clone"))
            {
                string name = cubes[i].name.Split('(')[0];
                Destroy(cubes[i]);
                GameObject.Find(name).transform.GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find(name).transform.GetComponent<BoxCollider>().enabled = true;
            }
        }

        switch (circle_index)
        {
            case 1:
                {
                    for (int i = 0; i < cubes0.Count; i++)
                    {
                        cubes0[i].transform.position = circle_o[i].transform.position;
                        cubes0[i].transform.position += Vector3.up / 2;
                        cubes0[i].transform.rotation = circle_o[i].transform.rotation;
                        cubes0[i].name = cubes0[i].GetComponent<Color_cube>().color_name;

                        string name = cubes0[i].name.Split('(')[0];
                        GameObject.Find(name).transform.GetComponent<MeshRenderer>().enabled = false;
                        GameObject.Find(name).transform.GetComponent<BoxCollider>().enabled = false;
                    }
                    circle_index--;
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < cubes1.Count; i++)
                    {
                        cubes1[i].transform.position = circle_o[i].transform.position;
                        cubes1[i].transform.position += Vector3.up / 2;
                        cubes1[i].transform.rotation = circle_o[i].transform.rotation;
                        cubes1[i].name = cubes1[i].GetComponent<Color_cube>().color_name;

                        string name = cubes1[i].name.Split('(')[0];
                        GameObject.Find(name).transform.GetComponent<MeshRenderer>().enabled = false;
                        GameObject.Find(name).transform.GetComponent<BoxCollider>().enabled = false;
                    }
                    circle_index--;
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < cubes2.Count; i++)
                    {
                        cubes2[i].transform.position = circle_o[i].transform.position;
                        cubes2[i].transform.position += Vector3.up / 2;
                        cubes2[i].transform.rotation = circle_o[i].transform.rotation;
                        cubes2[i].name = cubes2[i].GetComponent<Color_cube>().color_name;

                        string name = cubes2[i].name.Split('(')[0];
                        GameObject.Find(name).transform.GetComponent<MeshRenderer>().enabled = false;
                        GameObject.Find(name).transform.GetComponent<BoxCollider>().enabled = false;
                    }
                    circle_index--;
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < cubes2.Count; i++)
                    {
                        cubes3[i].transform.position = circle_o[i].transform.position;
                        cubes3[i].transform.position += Vector3.up / 2;
                        cubes3[i].transform.rotation = circle_o[i].transform.rotation;
                        cubes3[i].name = cubes3[i].GetComponent<Color_cube>().color_name;

                        string name = cubes3[i].name.Split('(')[0];
                        GameObject.Find(name).transform.GetComponent<MeshRenderer>().enabled = false;
                        GameObject.Find(name).transform.GetComponent<BoxCollider>().enabled = false;
                    }
                    circle_index--;
                    break;
                }
            default: break;
        }

        Change_text_in_circle();
    }

    public void Change_text_in_circle()
    {
        switch (circle_index)
        {
            case 0: 
                { 
                    text.transform.GetChild(0).gameObject.SetActive(true);
                    text.transform.GetChild(1).gameObject.SetActive(false);
                    text.transform.GetChild(2).gameObject.SetActive(false);
                    text.transform.GetChild(3).gameObject.SetActive(false);
                    text.transform.GetChild(4).gameObject.SetActive(false);
                    text.transform.GetChild(5).gameObject.SetActive(false);
                    text.transform.GetChild(6).gameObject.SetActive(false);
                    text.transform.GetChild(7).gameObject.SetActive(false);
                    break; 
                }
            case 1:
                {
                    text.transform.GetChild(0).gameObject.SetActive(false);
                    text.transform.GetChild(1).gameObject.SetActive(true);
                    text.transform.GetChild(2).gameObject.SetActive(false);
                    text.transform.GetChild(3).gameObject.SetActive(false);
                    text.transform.GetChild(4).gameObject.SetActive(false);
                    text.transform.GetChild(5).gameObject.SetActive(false);
                    text.transform.GetChild(6).gameObject.SetActive(false);
                    text.transform.GetChild(7).gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    text.transform.GetChild(0).gameObject.SetActive(false);
                    text.transform.GetChild(1).gameObject.SetActive(false);
                    text.transform.GetChild(2).gameObject.SetActive(true);
                    text.transform.GetChild(3).gameObject.SetActive(false);
                    text.transform.GetChild(4).gameObject.SetActive(false);
                    text.transform.GetChild(5).gameObject.SetActive(false);
                    text.transform.GetChild(6).gameObject.SetActive(false);
                    text.transform.GetChild(7).gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    text.transform.GetChild(0).gameObject.SetActive(false);
                    text.transform.GetChild(1).gameObject.SetActive(false);
                    text.transform.GetChild(2).gameObject.SetActive(false);
                    text.transform.GetChild(3).gameObject.SetActive(true);
                    text.transform.GetChild(4).gameObject.SetActive(false);
                    text.transform.GetChild(5).gameObject.SetActive(false);
                    text.transform.GetChild(6).gameObject.SetActive(false);
                    text.transform.GetChild(7).gameObject.SetActive(false);
                    break;
                }
            case 4:
                {
                    text.transform.GetChild(0).gameObject.SetActive(false);
                    text.transform.GetChild(1).gameObject.SetActive(false);
                    text.transform.GetChild(2).gameObject.SetActive(false);
                    text.transform.GetChild(3).gameObject.SetActive(false);
                    text.transform.GetChild(4).gameObject.SetActive(false);
                    text.transform.GetChild(5).gameObject.SetActive(false);
                    text.transform.GetChild(6).gameObject.SetActive(false);
                    text.transform.GetChild(7).gameObject.SetActive(true);
                    break;
                }
            default: break;
        }
    }

    private void Update()
    {
        if (problem_with_circle_so_wait)
        {
            timer += Time.deltaTime;

            if (timer > time_to_wait)
            {
                timer = 0;
                problem_with_circle_so_wait = false;
                Change_text_in_circle();
            }
        }
    }
}
