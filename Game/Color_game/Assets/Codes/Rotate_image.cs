using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_image : MonoBehaviour
{
    void Update()
    {
        this.transform.RotateAround(transform.position, Vector3.up, 0.5f);
    }
}
