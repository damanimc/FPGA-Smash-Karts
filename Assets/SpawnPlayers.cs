using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    public static SpawnPlayers instance;
    SpawnPoint[] spawnPoints;

   
  
    void Awake()
    {
       

        instance = this;
        spawnPoints = GetComponentsInChildren<SpawnPoint>();

    }

    public Transform GetSpawnPoint()
    {

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        
    }
}
