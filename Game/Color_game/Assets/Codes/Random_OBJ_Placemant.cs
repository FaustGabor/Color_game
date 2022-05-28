using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_OBJ_Placemant : MonoBehaviour
{
    // Start is called before the first frame update

    private static System.Random rng = new System.Random();
   
    //Ez a f�ggv�ny keverei �ssze a position list�nak az elemeit
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

    public List<GameObject> colorcubes = new List<GameObject>();    //A rendes sz�nes kock�k
    public List<GameObject> ghostcubes = new List<GameObject>();    //A kock�knak a helyei

    public Dictionary<string, Vector3> orginalpositions = new Dictionary<string, Vector3>();    //A kock�k alap helyzete (ahhoz kell hogy vissza tudjam �ket rakni a check sor�n)
    void RandomPlacement()
    {
        colorcubes = this.gameObject.GetComponent<Game_handler>().all_cubes;
        ghostcubes = this.gameObject.GetComponent<Game_handler>().ghost_cubes;

        List<Vector3> positionsCC = new List<Vector3>();    //A sz�nes kock�k poz�ci�ja
        List<Vector3> positionsGC = new List<Vector3>();    //A kock�k hely�nek a poz�ci�ja

        foreach (var item in colorcubes)
        {
            positionsCC.Add(item.transform.position);
        }
        //A k�t lista felt�lt�se az kezdeti poz�ci�kkal
        foreach (var item in ghostcubes)
        {
            positionsGC.Add(item.transform.position);
        }

        ShufflePos(positionsCC);    //Sz�nek kock�k �sszekever�se
        ShufflePos(positionsGC);    //Kokc�k hely�nek az �sszekever�se

        for (int i = 0; i < positionsCC.Count; i++)
        {
            colorcubes[i].transform.position = positionsCC[i];
        }
        //Az �j poz�ci�k haszn�l�sa
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
