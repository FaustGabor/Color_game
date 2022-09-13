using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject text;

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

    public void Move_cubes_to_circle(List<GameObject> cubes)
    {

        cubes = Short_Cubes(cubes);

        switch (circle_index)
        {
            case 0:
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.position = circle0[i].transform.position;
                        cubes[i].transform.position += Vector3.up / 2;
                        cubes[i].transform.rotation = circle0[i].transform.rotation;
                        cubes[i].GetComponent<Color_cube>().color_name = cubes[i].name;
                        cubes[i].name = "Cube" + circle0[i].name;
                        cubes0 = cubes;
                    }
                    circle_index++;
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.position = circle1[i].transform.position;
                        cubes[i].transform.position += Vector3.up / 2;
                        cubes[i].transform.rotation = circle1[i].transform.rotation;
                        cubes[i].GetComponent<Color_cube>().color_name = cubes[i].name;
                        cubes[i].name = "Cube" + circle1[i].name;
                        cubes1 = cubes;
                    }
                    circle_index++;
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.position = circle2[i].transform.position;
                        cubes[i].transform.position += Vector3.up / 2;
                        cubes[i].transform.rotation = circle2[i].transform.rotation;
                        cubes[i].GetComponent<Color_cube>().color_name = cubes[i].name;
                        cubes[i].name = "Cube" + circle2[i].name;
                        cubes2 = cubes;
                    }
                    circle_index++;
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.position = circle3[i].transform.position;
                        cubes[i].transform.position += Vector3.up / 2;
                        cubes[i].transform.rotation = circle3[i].transform.rotation;
                        cubes[i].GetComponent<Color_cube>().color_name = cubes[i].name;
                        cubes[i].name = "Cube" + circle3[i].name;
                        cubes3 = cubes;
                    }
                    circle_index++;
                    break;
                }
            default: break;
        }

        Change_text_in_circle();
    }

    public void Move_cubes_back()
    {
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
                    break; 
                }
            case 1:
                {
                    text.transform.GetChild(0).gameObject.SetActive(false);
                    text.transform.GetChild(1).gameObject.SetActive(true);
                    text.transform.GetChild(2).gameObject.SetActive(false);
                    text.transform.GetChild(3).gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    text.transform.GetChild(0).gameObject.SetActive(false);
                    text.transform.GetChild(1).gameObject.SetActive(false);
                    text.transform.GetChild(2).gameObject.SetActive(true);
                    text.transform.GetChild(3).gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    text.transform.GetChild(0).gameObject.SetActive(false);
                    text.transform.GetChild(1).gameObject.SetActive(false);
                    text.transform.GetChild(2).gameObject.SetActive(false);
                    text.transform.GetChild(3).gameObject.SetActive(true);
                    break;
                }
            default: break;
        }
    }
}
