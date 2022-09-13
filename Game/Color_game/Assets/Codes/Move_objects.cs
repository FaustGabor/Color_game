using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_objects : MonoBehaviour
{
    [SerializeField] private GameObject selected_obj;
    [SerializeField] private GameObject background;
    private bool adjust_pos = false;
    public List<GameObject> badcubes = new List<GameObject>();
    public List<GameObject> goodcubes = new List<GameObject>();

    [SerializeField] private GameObject selected_obj_shower;

    private Vector3 drag_start_mouse_position = Vector3.zero;
    private bool candrag = true;
    private float drag_start_cubes_position = 0;
    private GameObject cubes;

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

                        if (!SceneManager.GetActiveScene().name.Contains("Diamond_Game"))
                        {
                            selected_obj_shower.transform.rotation = hit.transform.rotation;
                        }
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

                    if (SceneManager.GetActiveScene().name.Contains("Diamond_Game"))
                    {
                        //A jó helyen lévõ kockák
                        if (selected_obj.name == hit.transform.name)
                        {
                            //Ha eddig rossz helyen volt akkor azt abból a listávól kitörlöm
                            if (badcubes.Contains(selected_obj))
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
                    }

                    if (!SceneManager.GetActiveScene().name.Contains("Diamond_Game"))
                    {
                        selected_obj.transform.rotation = hit.transform.rotation;
                    }

                    selected_obj.transform.position = new Vector3(hit.transform.gameObject.transform.position.x, selected_obj.transform.position.y, hit.transform.gameObject.transform.position.z);
                    selected_obj.transform.position += Vector3.down / 2;
                    selected_obj = null;
                    selected_obj_shower.gameObject.SetActive(false);
                }
            }
        }
    }

    void Colour_wheel_Part()
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
                        if (hit.transform.gameObject.name != "Plane") // kocka kiválasztása
                        {
                            selected_obj = hit.transform.gameObject;

                            selected_obj_shower.gameObject.SetActive(true);
                            selected_obj_shower.transform.position = selected_obj.transform.position;
                            selected_obj_shower.transform.position += Vector3.down * (0.3f);
                            selected_obj_shower.transform.rotation = hit.transform.rotation;
                            candrag = false;
                        }
                        else // Kockák oldalra mozgatásának engedélyezése
                        {
                            drag_start_mouse_position = Input.mousePosition;
                            drag_start_cubes_position = cubes.transform.position.x;
                            candrag = true;
                        }
                    }
                }
                else
                {
                    if (hit.transform.gameObject.name == "Plane" || hit.transform.gameObject.tag == "Drag") // kocka vissza rakása
                    {
                        if (selected_obj.name.Contains("Clone"))
                        {
                            string name = selected_obj.name.Split('(')[0];
                            Destroy(selected_obj);
                            GameObject.Find(name).transform.GetComponent<MeshRenderer>().enabled = true;
                            GameObject.Find(name).transform.GetComponent<BoxCollider>().enabled = true;
                        }
                        selected_obj = null;
                        selected_obj_shower.gameObject.SetActive(false);
                    }
                    else  
                    {
                        if (selected_obj.name.Contains("Clone")) // kocka arrébb rakása a coloure wheel-en
                        {
                            selected_obj.transform.rotation = hit.transform.rotation;
                            selected_obj.transform.position = new Vector3(hit.transform.gameObject.transform.position.x, selected_obj.transform.position.y, hit.transform.gameObject.transform.position.z);
                            selected_obj.GetComponent<Color_cube>().gray_partner = hit.transform.name;
                            selected_obj = null;
                            selected_obj_shower.gameObject.SetActive(false);
                            
                        }
                        else // kocka lerakás a colour wheel-re
                        {
                            GameObject new_obj = Instantiate(selected_obj, new Vector3(hit.transform.gameObject.transform.position.x, selected_obj.transform.position.y, hit.transform.gameObject.transform.position.z), hit.transform.rotation);
                            new_obj.GetComponent<Color_cube>().gray_partner = hit.transform.name;
                            selected_obj.transform.GetComponent<MeshRenderer>().enabled = false;
                            selected_obj.transform.GetComponent<BoxCollider>().enabled = false;
                            selected_obj = null;
                            selected_obj_shower.gameObject.SetActive(false);
                        }
                    }
                }
            }

            if (Input.GetMouseButton(0)) // Kocka mozgatása oldalra
            {
                if (selected_obj == null && candrag) 
                {
                    if (hit.transform.gameObject.tag != "Ghost_Cubes")
                    {
                        if (hit.transform.gameObject.name == "Plane")
                        {
                            cubes.transform.position = new Vector3((drag_start_cubes_position+(drag_start_mouse_position.x - Input.mousePosition.x) / 50 ), 0, 3.72f);
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Diamond_Game"))
            DiamondPart();
        else if (SceneManager.GetActiveScene().name.Contains("Colour_org"))
            Colour_wheel_Part();
        else
            GreyScalePart();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Colour_org")) cubes = GameObject.Find("Cubes");
    }
}
