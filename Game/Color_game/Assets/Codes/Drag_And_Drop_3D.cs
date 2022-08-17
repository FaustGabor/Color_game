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

    [SerializeField] private GameObject selected_obj_shower;

    void GreyScalePart()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selected_obj == null) // Színes kockára kattintás esetén lementi a kockát
                {
                    if (hit.transform.gameObject.tag != "Grey_scale" && hit.transform.gameObject.tag != "Spawn")
                    {
                        selected_obj = hit.transform.gameObject;
                        background.SetActive(true);
                        selected_obj_shower.gameObject.SetActive(true);
                        selected_obj_shower.transform.position = selected_obj.transform.position;
                        selected_obj_shower.transform.position += Vector3.up/2;
                        selected_obj.transform.position += new Vector3(0, 0.5f, 0);
                    }
                }
                else // Lementett kockát lerakja a kattintás helyére (de úgy hogy a kattintásnál csak a Z koordinátát nézi [a skála mellett marad])
                {
                    float z = hit.point.z;
                    if (z > 84.158) z = 84.158f;
                    if (z < 77.757) z = 77.757f;

                    selected_obj.transform.position += new Vector3(0, -0.5f, 0);
                    selected_obj.transform.position = new Vector3(selected_obj.transform.position.x, selected_obj.transform.position.y, z);
                    selected_obj.GetComponent<Color_cube>().adjust_pos = true; // emiatt kerül jó helyre, és megnézi hogy a megfelelõ partner mellett van-e
                    selected_obj = null;
                    adjust_pos = true;
                    background.SetActive(false);
                    selected_obj_shower.gameObject.SetActive(false);
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
                        selected_obj.transform.position += Vector3.up / 2;

                        selected_obj_shower.gameObject.SetActive(true);
                        selected_obj_shower.transform.position = selected_obj.transform.position;
                        selected_obj_shower.transform.position += Vector3.down * (0.3f);
                    }
                }
                else
                {
                    //Ne lesennek egymásra rakni a kockákat
                    if(hit.transform.gameObject.tag == "Drag")
                    {
                        selected_obj.transform.position += Vector3.down / 2;
                        selected_obj = null;
                        selected_obj_shower.gameObject.SetActive(false);
                        return;
                    }
                    //A jó helyen lévõ kockák
                    if(selected_obj.name == hit.transform.name)
                    {
                        //Ha eddig rossz helyen volt akkor azt abból a listávól kitörlöm
                        if(badcubes.Contains(selected_obj))
                        {
                            badcubes.Remove(selected_obj);
                        }
                        //Ha még nem szerepelt a jó helyen lévõk között akkor hozzáadom
                        if (!goodcubes.Contains(selected_obj))
                            goodcubes.Add(selected_obj);
                    }
                    //A rossz helyen lévõ kockák
                    else
                    {
                        //Ha még nem szerepelt a rossz helyen lévõk között akkor hozzáadom
                        if (!badcubes.Contains(selected_obj))
                            badcubes.Add(selected_obj);
                        //Ha eddig jó helyen volt de át lett rakva rosszra akkor kitörlöm a jók közül
                        if (goodcubes.Contains(selected_obj))
                            goodcubes.Remove(selected_obj);
                    }
                        
                    selected_obj.transform.position = new Vector3(hit.transform.gameObject.transform.position.x, selected_obj.transform.position.y, hit.transform.gameObject.transform.position.z);
                    selected_obj.transform.position += Vector3.down / 2;
                    selected_obj = null;
                    selected_obj_shower.gameObject.SetActive(false);
                }
            }
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Diamond_Game"))
            DiamondPart();
        else
            GreyScalePart();
    }
}
