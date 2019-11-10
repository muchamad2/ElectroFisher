using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject[] spownpoint;
    public GameObject[] riverFishObject;
    public GameObject[] seaFishObject;
    void Start()
    {
        Spawn();
    }
    void Spawn()
    {
        foreach (var sp in spownpoint)
        {
            GameObject obj;
            if (GameUtility.environmentType == EnvironmentType.Forest)
            {
                int index = getRiverFish();
                obj = Instantiate(riverFishObject[index], sp.transform.position, Quaternion.identity);
            }
            else if (GameUtility.environmentType == EnvironmentType.Sea)
            {
                int index = getSeaFish();
                obj = Instantiate(seaFishObject[index], sp.transform.position, Quaternion.identity);
            }

        }
    }
    public int getRiverFish(){
        int rand = UnityEngine.Random.Range(0,riverFishObject.Length-1);
        return rand;
    }
    int getSeaFish(){
        int rand = UnityEngine.Random.Range(0,seaFishObject.Length-1);
        return rand;
    }

}
