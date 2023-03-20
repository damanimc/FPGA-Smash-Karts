using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    public static SpawnPlayers instance;
    SpawnPoint[] spawnPoints;
    GameObject player;
    public GameObject playerPrefab;
    public SmoothFollow camera;

    public void Start()
    {
        instance = this;
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        Respawn();
    }

    public void Respawn()
    {
        if (player)
        {

            PhotonNetwork.Destroy(player);
            
        }
        Transform spawn = spawnPoints[Random.RandomRange(0, spawnPoints.Length)].transform;
        player = PhotonNetwork.Instantiate(playerPrefab.name, spawn.position, spawn.rotation);
        player.GetComponent<MovementScript>().spawnManager = this;
        camera.targetObject = player;
        camera.target = player.transform;
        Debug.Log("Spawning player");
    }
}
