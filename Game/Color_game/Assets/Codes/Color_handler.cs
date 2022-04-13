using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_handler : MonoBehaviour
{
    public List<GameObject> collided_obj;

    public bool adjust_pos;

    public void Add_to_collided_list(GameObject obj)
    {
        collided_obj.Add(obj);
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

            collided_obj.Clear();

            adjust_pos = false;

            return pos_z;
        }
        else
        {
            return this.transform.position.z;
        }    
    }

    private void LateUpdate()
    {
        if (adjust_pos)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Get_position_z());
        }
    }

    private void Start()
    {
        adjust_pos = false;
    }
}
