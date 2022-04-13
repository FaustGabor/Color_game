using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gray_scale_collider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Background")
        {
            other.gameObject.GetComponent<Color_handler>().Add_to_collided_list(this.gameObject);
        }
    }
}
