using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Material_Color : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material.SetColor("red", Color.red);
    }
}
