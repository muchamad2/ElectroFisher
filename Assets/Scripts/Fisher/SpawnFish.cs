using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject[] spownpoint;
    public GameObject riverFishObject;
    public GameObject seaFishObject;
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
                obj = Instantiate(riverFishObject, sp.transform.position, Quaternion.identity);
            }
            else if (GameUtility.environmentType == EnvironmentType.Sea)
            {
                obj = Instantiate(seaFishObject, sp.transform.position, Quaternion.identity);
            }

        }
    }

}
