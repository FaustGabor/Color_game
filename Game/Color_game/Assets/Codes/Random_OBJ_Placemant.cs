using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_OBJ_Placemant : MonoBehaviour
{
    // Start is called before the first frame update

    private static System.Random rng = new System.Random();
   
    //Ez a függvény keverei össze a position listának az elemeit
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

    public List<GameObject> colorcubes = new List<GameObject>();    //A rendes színes kockák
    public List<GameObject> ghostcubes = new List<GameObject>();    //A kockáknak a helyei

    public Dictionary<string, Vector3> orginalpositions = new Dictionary<string, Vector3>();    //A kockák alap helyzete (ahhoz kell hogy vissza tudjam õket rakni a check során)
    void RandomPlacement()
    {
        colorcubes = this.gameObject.GetComponent<Game_handler>().all_cubes;
        ghostcubes = this.gameObject.GetComponent<Game_handler>().ghost_cubes;

        List<Vector3> positionsCC = new List<Vector3>();    //A színes kockák pozíciója
        List<Vector3> positionsGC = new List<Vector3>();    //A kockák helyének a pozíciója

        foreach (var item in colorcubes)
        {
            positionsCC.Add(item.transform.position);
        }
        //A két lista feltöltése az kezdeti pozíciókkal
        foreach (var item in ghostcubes)
        {
            positionsGC.Add(item.transform.position);
        }

        ShufflePos(positionsCC);    //Színek kockák összekeverése
        ShufflePos(positionsGC);    //Kokcák helyének az összekeverése

        for (int i = 0; i < positionsCC.Count; i++)
        {
            colorcubes[i].transform.position = positionsCC[i];
        }
        //Az új pozíciók használása
        for (int i = 0; i < positionsGC.Count; i++)
        {
            ghostcubes[i].transform.position = positionsGC[i];
        }

        foreach (var item in colorcubes)
        {
            orginalpositions.Add(item.name, item.transform.position);
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
