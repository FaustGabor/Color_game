using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnable : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }
}
