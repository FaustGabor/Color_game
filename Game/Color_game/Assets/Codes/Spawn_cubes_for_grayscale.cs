using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_cubes_for_grayscale : MonoBehaviour
{
    public GameObject game_handler;

    private bool left;

    void Start()
    {
        left = false;
        if (this.gameObject.name.Contains("left")) left = true;

        Spawn();
    }

    private void Spawn()
    {
        GameObject spawn_obj = game_handler.GetComponent<Game_handler>().Next_cube(left); // lekéri a következõ kockát
        if(spawn_obj != null)
        game_handler.GetComponent<Game_handler>().Add_Cube(left, Instantiate(spawn_obj, this.transform.position + Vector3.up, this.transform.rotation));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Background")
        {
            Spawn();
        }
    }
}
