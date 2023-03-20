using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    public static SpawnPlayers instance;
    SpawnPoint[] spawnPoints;

    public GameObject playerPrefab;
    public SmoothFollow camera;

    public void Start()
    {
        instance = this;
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        Transform spawn = spawnPoints[Random.RandomRange(0,spawnPoints.Length)].transform;
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawn.position, spawn.rotation);
        camera.targetObject = player;
        camera.target = player.transform;
    }
}
