using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_cube : MonoBehaviour
{
    [SerializeField] private string color_name;
    [SerializeField] private string color_hexa_num;

    public string gray_partner;
    public bool at_right_position;
    public string[] partners;

    // ----------------------------------- GRAY SCALE ---------------------
    private float timer;
    public List<GameObject> collided_obj;
    public bool adjust_pos;

    public void Add_to_collided_list(GameObject obj)
    {
        collided_obj.Add(obj);
    }

    public void Remove_to_collided_list(GameObject obj)
    {
        collided_obj.Remove(obj);
    }

    public float Get_position_z()
    {
        if (collided_obj.Count > 0)
        {
            float pos_z = 0;

            foreach (var item in collided_obj)
            {
                pos_z += item.transform.position.z;
            }

            pos_z /= collided_obj.Count;

            //collided_obj.Clear();

            adjust_pos = false;

            return pos_z;
        }
        else
        {
            return this.transform.position.z;
        }
    }

    public void Check_Right_Position_GS()
    {
        partners = gray_partner.Split(',');

        if (partners.Length == 1)
        {
            foreach (var item in collided_obj)
            {
                if (!partners[0].Contains(item.name))
                {
                    at_right_position = false;
                    break;
                }
                at_right_position = true;
            }
        }
        else
        {

            if (collided_obj.Count == 1) 
            { 
                at_right_position = false;
            }
            else
            {
                if (partners[0].Contains(collided_obj[0].name) || partners[0].Contains(collided_obj[1].name))
                {
                    if (partners[1].Contains(collided_obj[0].name) || partners[1].Contains(collided_obj[1].name))
                    {
                        at_right_position = true;
                        return;
                    }
                }

                at_right_position = false;
            }
        }
    }
    // ----------------------------------- END OF GRAY SCALE ---------------------

    private void Start()
    {
        at_right_position = false;

        adjust_pos = false;
        timer = 0;
    }

    private void Update()
    {
        // ------ GREY SCALE--------
        timer += Time.deltaTime;

        if (timer > 0.2) // colliders need time to register object
        {
            timer = 0;

            if (adjust_pos)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Get_position_z());
                Check_Right_Position_GS();
            }
        }
        // ------ END OF GREY SCALE--------
    }
}
