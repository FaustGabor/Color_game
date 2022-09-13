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
                if (selected_obj == null) // Sz�nes kock�ra kattint�s eset�n lementi a kock�t
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
                else // Lementett kock�t lerakja a kattint�s hely�re (de �gy hogy a kattint�sn�l csak a Z koordin�t�t n�zi [a sk�la mellett marad])
                {
                    float z = hit.point.z;
                    if (z > 84.158) z = 84.158f;
                    if (z < 77.757) z = 77.757f;

                    selected_obj.transform.position += new Vector3(0, -0.5f, 0);
                    selected_obj.transform.position = new Vector3(selected_obj.transform.position.x, selected_obj.transform.position.y, z);
                    selected_obj.GetComponent<Color_cube>().adjust_pos = true; // emiatt ker�l j� helyre, �s megn�zi hogy a megfelel� partner mellett van-e
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
                    //Ne lesennek egym�sra rakni a kock�kat
                    if(hit.transform.gameObject.tag == "Drag")
                    {
                        selected_obj.transform.position += Vector3.down / 2;
                        selected_obj = null;
                        selected_obj_shower.gameObject.SetActive(false);
                        return;
                    }

                    if (SceneManager.GetActiveScene().name.Contains("Diamond_Game"))
                    {
                        //A j� helyen l�v� kock�k
                        if (selected_obj.name == hit.transform.name)
                        {
                            //Ha eddig rossz helyen volt akkor azt abb�l a list�v�l kit�rl�m
                            if (badcubes.Contains(selected_obj))
                            {
                                badcubes.Remove(selected_obj);
                            }
                            //Ha m�g nem szerepelt a j� helyen l�v�k k�z�tt akkor hozz�adom
                            if (!goodcubes.Contains(selected_obj))
                                goodcubes.Add(selected_obj);
                        }
                        //A rossz helyen l�v� kock�k
                        else
                        {
                            //Ha m�g nem szerepelt a rossz helyen l�v�k k�z�tt akkor hozz�adom
                            if (!badcubes.Contains(selected_obj))
                                badcubes.Add(selected_obj);
                            //Ha eddig j� helyen volt de �t lett rakva rosszra akkor kit�rl�m a j�k k�z�l
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
                        if (hit.transform.gameObject.name != "Plane") // kocka kiv�laszt�sa
                        {
                            selected_obj = hit.transform.gameObject;

                            selected_obj_shower.gameObject.SetActive(true);
                            selected_obj_shower.transform.position = selected_obj.transform.position;
                            selected_obj_shower.transform.position += Vector3.down * (0.3f);
                            selected_obj_shower.transform.rotation = hit.transform.rotation;
                            candrag = false;
                        }
                        else // Kock�k oldalra mozgat�s�nak enged�lyez�se
                        {
                            drag_start_mouse_position = Input.mousePosition;
                            drag_start_cubes_position = cubes.transform.position.x;
                            candrag = true;
                        }
                    }
                }
                else
                {
                    if (hit.transform.gameObject.name == "Plane" || hit.transform.gameObject.tag == "Drag") // kocka vissza rak�sa
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
                        if (selected_obj.name.Contains("Clone")) // kocka arr�bb rak�sa a coloure wheel-en
                        {
                            selected_obj.transform.rotation = hit.transform.rotation;
                            selected_obj.transform.position = new Vector3(hit.transform.gameObject.transform.position.x, selected_obj.transform.position.y, hit.transform.gameObject.transform.position.z);
                            selected_obj.GetComponent<Color_cube>().gray_partner = hit.transform.name;
                            selected_obj = null;
                            selected_obj_shower.gameObject.SetActive(false);
                            
                        }
                        else // kocka lerak�s a colour wheel-re
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

            if (Input.GetMouseButton(0)) // Kocka mozgat�sa oldalra
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
