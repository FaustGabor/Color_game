using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drag_And_Drop_3D : MonoBehaviour
{
    [SerializeField] private GameObject selected_obj;
    [SerializeField] private GameObject background;
    private bool adjust_pos = false;
    public List<GameObject> badcubes = new List<GameObject>();
    public List<GameObject> goodcubes = new List<GameObject>();


    void GreyScalePart()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selected_obj == null)
                {
                    if (hit.transform.gameObject.tag != "Grey_scale" && hit.transform.gameObject.tag != "Spawn")
                    {
                        selected_obj = hit.transform.gameObject;
                        background.SetActive(true);
                    }
                }
                else
                {
                    float z = hit.point.z;
                    if (z > 84.158) z = 84.158f;
                    if (z < 77.757) z = 77.757f;

                    selected_obj.transform.position = new Vector3(selected_obj.transform.position.x, selected_obj.transform.position.y, z);
                    selected_obj.GetComponent<Color_cube>().adjust_pos = true;
                    selected_obj = null;
                    adjust_pos = true;
                    background.SetActive(false);
                }
            }
        }
    }

    void DiamondPart()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selected_obj == null)
                {
                    if (hit.transform.gameObject.tag != "Ghost_Cubes")
                    {
                        selected_obj = hit.transform.gameObject;
                    }
                }
                else
                {
                    if(hit.transform.gameObject.tag == "Drag")
                    {
                        selected_obj = null;
                        Debug.Log("Over");
                        return;
                    }
                    if(selected_obj.name == hit.transform.name)
                    {
                        Debug.Log("Good");
                        if(badcubes.Contains(selected_obj))
                        {
                            badcubes.Remove(selected_obj);
                        }
                        if (!goodcubes.Contains(selected_obj))
                            goodcubes.Add(selected_obj);
                    }
                    else
                    {
                        if (!badcubes.Contains(selected_obj))
                            badcubes.Add(selected_obj);
                        if (goodcubes.Contains(selected_obj))
                            goodcubes.Remove(selected_obj);
                        Debug.Log("NotGood");
                    }
                        
                    selected_obj.transform.position = new Vector3(hit.transform.gameObject.transform.position.x, selected_obj.transform.position.y, hit.transform.gameObject.transform.position.z);
                    selected_obj = null;
                }
            }
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Diamond_Game")
            DiamondPart();
        else
            GreyScalePart();
    }
}
