using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_OBJ_Placemant : MonoBehaviour
{
    // Start is called before the first frame update

    private static System.Random rng = new System.Random();
    public static void ShufflePos(List<Vector3> pos)
    {
        int n = pos.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Vector3 value = pos[k];
            pos[k] = pos[n];
            pos[n] = value;
        }
    }

    void RandomPlacement()
    {
        List<GameObject> colorcubes = this.gameObject.GetComponent<Game_handler>().all_cubes;
        
        List<Vector3> positions = new List<Vector3>();

        foreach (var item in colorcubes)
        {
            positions.Add(item.transform.position);
        }

        ShufflePos(positions);


        for (int i = 0; i < positions.Count; i++)
        {
            colorcubes[i].transform.position = positions[i];
        }
    }

    
    void Start()
    {

        RandomPlacement();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
