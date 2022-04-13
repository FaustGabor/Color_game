using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_And_Drop_3D : MonoBehaviour
{
    [SerializeField] private GameObject selected_obj;
    [SerializeField] private GameObject background;
    private bool adjust_pos = false;

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selected_obj == null)
                {
                    selected_obj = hit.transform.gameObject;
                    background.SetActive(true);
                }
                else
                {
                    selected_obj.transform.position = hit.point;
                    selected_obj.GetComponent<Color_handler>().adjust_pos = true;
                    selected_obj = null;
                    adjust_pos = true;
                    background.SetActive(false);
                }
            }
        }
    }
}
