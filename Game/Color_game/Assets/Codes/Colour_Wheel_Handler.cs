using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour_Wheel_Handler : MonoBehaviour
{
    private int circle = 0;

    [SerializeField] private List<GameObject> circle0;
    [SerializeField] private List<GameObject> circle1;
    [SerializeField] private List<GameObject> circle2;
    [SerializeField] private List<GameObject> circle3;

    public void Move_cubes_to_circle(List<GameObject> cubes)
    {

        cubes = Short_Cubes(cubes);

        switch (circle)
        {
            case 0:
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.position = circle0[i].transform.position;
                        cubes[i].transform.position += Vector3.up / 2;
                        cubes[i].transform.rotation = circle0[i].transform.rotation;
                        cubes[i].name = "Cube" + circle0[i].name;
                    }
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.position = circle1[i].transform.position;
                        cubes[i].transform.position += Vector3.up / 2;
                        cubes[i].transform.rotation = circle1[i].transform.rotation;
                        cubes[i].name = "Cube" + circle1[i].name;
                    }
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.position = circle2[i].transform.position;
                        cubes[i].transform.position += Vector3.up / 2;
                        cubes[i].transform.rotation = circle2[i].transform.rotation;
                        cubes[i].name = "Cube" + circle2[i].name;
                    }
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.position = circle3[i].transform.position;
                        cubes[i].transform.position += Vector3.up / 2;
                        cubes[i].transform.rotation = circle3[i].transform.rotation;
                        cubes[i].name = "Cube" + circle3[i].name;
                    }
                    break;
                }
            default: break;
        }
        circle++;
    }

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
}
