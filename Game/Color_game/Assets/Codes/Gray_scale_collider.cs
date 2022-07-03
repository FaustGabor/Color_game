using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gray_scale_collider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Background" && other.gameObject.tag != "Spawn" && other.gameObject.tag != "Grey_scale")
        {
            other.gameObject.GetComponent<Color_cube>().Add_to_collided_list(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Background" && other.gameObject.tag != "Spawn" && other.gameObject.tag != "Grey_scale")
        {
            other.gameObject.GetComponent<Color_cube>().Remove_to_collided_list(this.gameObject);
        }
    }
}
