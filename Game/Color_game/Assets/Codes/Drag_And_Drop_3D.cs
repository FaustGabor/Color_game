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
                    //A j� helyen l�v� kock�k
                    if(selected_obj.name == hit.transform.name)
                    {
                        //Ha eddig rossz helyen volt akkor azt abb�l a list�v�l kit�rl�m
                        if(badcubes.Contains(selected_obj))
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
