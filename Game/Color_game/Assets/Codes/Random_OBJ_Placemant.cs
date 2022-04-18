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
        List<GameObject> ghostcubes = this.gameObject.GetComponent<Game_handler>().ghost_cubes;
        
        List<Vector3> positionsCC = new List<Vector3>();
        List<Vector3> positionsGC = new List<Vector3>();

        foreach (var item in colorcubes)
        {
            positionsCC.Add(item.transform.position);
        }

        foreach (var item in ghostcubes)
        {
            positionsGC.Add(item.transform.position);
        }

        ShufflePos(positionsCC);
        ShufflePos(positionsGC);


        for (int i = 0; i < positionsCC.Count; i++)
        {
            colorcubes[i].transform.position = positionsCC[i];
        }

        for (int i = 0; i < positionsGC.Count; i++)
        {
            ghostcubes[i].transform.position = positionsGC[i];
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
